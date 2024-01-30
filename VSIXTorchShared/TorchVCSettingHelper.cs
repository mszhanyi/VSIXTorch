namespace VSIXTorch
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using static Microsoft.VisualStudio.Shell.ThreadedWaitDialogHelper;

    public class TorchSettingException: Exception
    {
        public TorchSettingException(string name)
            : base(String.Format("{0}", name))
        {

        }
    }

    public class TorchVCSettingHelper
    {
        private string jsonfile;
        private string torch_version;
        private string torch_cuda_version;
        private string torch_full_version;
        private string cudaPath;
        private string nvToolsExtPath;
        public TorchVCSettingHelper(string torch_version, string torch_cuda_version, string torch_full_version)
        {
            this.torch_version = torch_version;
            this.torch_cuda_version = torch_cuda_version;
            this.torch_full_version = torch_full_version;
            this.cudaPath = Environment.GetEnvironmentVariable("CUDA_PATH");
            this.nvToolsExtPath = Environment.GetEnvironmentVariable("NVTOOLSEXT_PATH");

            var assembly_path= this.GetType().Assembly.Location;
            var assembly_dir = Path.GetDirectoryName(assembly_path);
            this.jsonfile = Path.Combine(assembly_dir, "vcproject.json");
            if (!File.Exists(this.jsonfile))
            {
                throw new TorchSettingException(String.Format("{0} doesn't exist! You might need to reinstall the Torch extension", this.jsonfile));
            }
        }

        // This mehod is called to get AdditionalIncludeDirectories
        public string TorchIncludeDirs(string prefix)
        {
            string ret_dirs = String.Empty;
            using (StreamReader file = File.OpenText(this.jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    var root = JToken.ReadFrom(reader);
                    var projects = this.TorchProjects(root);
                    var torch_version = this.TorchVersion(projects);
                    var cuda_version = this.TorchCUDAVersion(torch_version);
                    var include_dirs = cuda_version["includes"];
                    var torch_dir_array = include_dirs["torch_dirs"] as JArray;
                    var torch_dirs = this.CombinePaths(prefix, torch_dir_array);

                    var cuda_include = include_dirs["cuda_dir"] as JArray;
                    var cuda_dirs = string.Empty;
                    if (cuda_include != null)
                    {
                        cuda_dirs = this.CombinePaths(this.cudaPath, cuda_include);
                    }

                    var nvtools_dir = include_dirs["nvtools_dir"] as JArray;
                    var nvtools_ext_dirs = string.Empty;
                    if (nvtools_dir != null)
                    {
                        nvtools_ext_dirs = this.CombinePaths(this.nvToolsExtPath, nvtools_dir);
                    }

                    string[] all_includes = { torch_dirs, cuda_dirs, nvtools_ext_dirs };
                    ret_dirs = string.Join(";", all_includes);
                }
            }
            return ret_dirs;
        }

        public string TorchDependLibs(string prefix)
        {
            string ret_dirs = String.Empty;
            using (StreamReader file = File.OpenText(this.jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    var root = JToken.ReadFrom(reader);
                    var projects = this.TorchProjects(root);
                    var torch_version = this.TorchVersion(projects);
                    var cuda_version = this.TorchCUDAVersion(torch_version);
                    var libs = cuda_version["libs"];
                    var torch_lib_array = libs["torch_libs"] as JArray;
                    prefix = Path.Combine(prefix, "libtorch\\lib");
                    var torch_dirs = this.CombinePaths(prefix, torch_lib_array);

                    var cuda_include = libs["cuda_libs"] as JArray;
                    var cuda_dirs = string.Empty;
                    if (cuda_include != null)
                    {
                        var cuda_prefix = Path.Combine(this.cudaPath, "lib\\x64");
                        cuda_dirs = this.CombinePaths(cuda_prefix, cuda_include);
                    }

                    var nvtools_dir = libs["nvtools_libs"] as JArray;
                    var nvtools_ext_dirs = string.Empty;
                    if (nvtools_dir != null)
                    {
                        var nv_prefix = Path.Combine(this.nvToolsExtPath, "lib\\x64");
                        nvtools_ext_dirs = this.CombinePaths(nv_prefix, nvtools_dir);
                    }

                    string[] all_includes = { torch_dirs, cuda_dirs, nvtools_ext_dirs };
                    ret_dirs = string.Join(";", all_includes);
                }
            }
            return ret_dirs;
        }

        public string TorchAdditionalLinkOptions()
        {
            if (string.Equals(this.torch_cuda_version.ToLower(), "cpu"))
            {
                return string.Empty;
            }

            string ret_str = string.Empty;
            using (StreamReader file = File.OpenText(this.jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    var root = JToken.ReadFrom(reader);
                    var projects = this.TorchProjects(root);
                    var torch_version = this.TorchVersion(projects);
                    var cuda_version = this.TorchCUDAVersion(torch_version);
                    ret_str = (cuda_version["link_options"]).ToString();
                }
            }
            return ret_str;
        }

        private JToken TorchProjects(JToken root)
        {
            var jtoken = root["projects"];
            if (jtoken == null)
            {
                throw new TorchSettingException(String.Format("All settings should be under porjects"));
            }
            return jtoken;
        }

        private JToken TorchVersion(JToken root)
        {
            var version = this.torch_version;
            var jtoken = root[version];
            if (jtoken == null)
            {
                throw new TorchSettingException(String.Format("torch {0} isn't supported yet", version));
            }
            return jtoken;
        }

        private JToken TorchCUDAVersion(JToken torch_version)
        {
            var cuda_version = torch_cuda_version.ToLower();
            var jtoken = torch_version[cuda_version];
            if (jtoken == null)
            {
                throw new TorchSettingException(String.Format("cuda {0} isn't supported yet", this.torch_cuda_version));
            }
            return jtoken;
        }

        private bool Exist(string component)
        {
            var biggeridx = component.IndexOf(">=");
            if ( biggeridx > 0)
            {
                var version = component.Substring(biggeridx + 2);
                var r = new Regex(@"\d+\.\d+");
                Match m = r.Match(version);
                if (m.Success)
                {
                    float d_version, current_version;
                    float.TryParse(version, out d_version);
                    float.TryParse(this.torch_version, out current_version);
                    if (current_version < d_version)
                    {
                        return false;
                    }
                }
                else
                {
                    throw new TorchSettingException(String.Format("{0} format isn't correct", component));
                }
            }
            return true;
        }

        // remove the possible >=
        private string Combine(string prefix, string component)
        {
            var biggeridx = component.IndexOf(">=");
            if (biggeridx > 0)
            {
                component = component.Substring(0, biggeridx);
            }
            return Path.Combine(prefix, component);
        }

        private string CombinePaths(string prefix, JArray paths)
        {
            var d1 = paths.ToObject<List<string>>();
            var d2 = d1.Where(x => Exist(x)).Select(x => this.Combine(prefix, x)).ToList();
            return string.Join(";", d2);
        }
    }
}
