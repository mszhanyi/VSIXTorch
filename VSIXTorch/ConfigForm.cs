namespace VSIXTorch
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    public partial class ConfigForm : Form
    {
        private string link_options;
        private string torch_release_libs;
        private string torch_debug_libs;
        private string torch_release_includes;
        private string torch_debug_includes;

        public ConfigForm()
        {
            InitializeComponent();
            lbl_cudapath.Text = Environment.GetEnvironmentVariable("CUDA_PATH");
            lbl_nvtoolspath.Text = Environment.GetEnvironmentVariable("NVTOOLSEXT_PATH");

            this.lbl_debug_torchversion.Text = String.Empty;
            this.lbl_release_torchversion.Text = String.Empty;
            this.txt_debug_dir.MouseHover += new System.EventHandler(this.HoverDir);
            this.txt_release_dir.MouseHover += new System.EventHandler(this.HoverDir);

            this.LoadSelection();
            if (!String.IsNullOrWhiteSpace(this.DebugLibPATH))
            {
                this.ValidateLibPath(this.DebugLibPATH, true);
            }
            if (!String.IsNullOrWhiteSpace(this.ReleaseLibPATH))
            {
                this.ValidateLibPath(this.ReleaseLibPATH, false);
            }
        }

        public string TorchCUDAVersion
        {
            get {
                return this.ExtractVersionText(@"(?<=cu)\d*|cpu");
            }
        }

        public string TorchVersion
        {
            get
            {
                return this.ExtractVersionText(@"(\d+\.\d+\.\d)");
            }
        }

        private string TorchFullVersion
        {
            get
            {
                var fullversion = String.IsNullOrWhiteSpace(this.lbl_debug_torchversion.Text) ? this.lbl_release_torchversion.Text : this.lbl_debug_torchversion.Text;
                return fullversion.TrimEnd('\r', '\n');
            }
        }
        private string ExtractVersionText(string regstr)
        {
            var torchFullVer = this.TorchFullVersion;
            var r = new Regex(regstr);
            Match m = r.Match(torchFullVer);
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return string.Empty;
            }
        }

        public string NvToolsPATH
        {
            get
            {
                return this.lbl_nvtoolspath.Text;
            }
        }
        public string DebugLibPATH
        {
            get
            {
                return this.txt_debug_dir.Text;
            }
        }

        public string ReleaseLibPATH
        {
            get
            {
                return this.txt_release_dir.Text;
            }
        }

        public string TorchDebugLibs
        {
            get
            {
                return this.torch_debug_libs;
            }
        }

        public string TorchReleaseLibs
        {
            get
            {
                return this.torch_release_libs;
            }
        }

        public string TorchDebugIncludes
        {
            get
            {
                return this.torch_debug_includes;
            }
        }

        public string TorchReleaseIncludes
        {
            get
            {
                return this.torch_release_includes;
            }
        }

        public string TorchLinkOptions
        {
            get
            {
                return this.link_options;
            }
        }

        public bool ExistLibPATH
        {
            get
            {
                return !(String.IsNullOrWhiteSpace(this.DebugLibPATH) && String.IsNullOrWhiteSpace(this.ReleaseLibPATH));
            }
        }


        private const string regKey = "Software\\Microsoft\\VisualStudio\\TorchVSTemplate";

        #region set lib folder
        private void ChooseDebugLibFolder(object sender, EventArgs e)
        {
            if (dlg_libdir.ShowDialog() == DialogResult.OK)
            {
                if (this.ValidateLibPath(dlg_libdir.SelectedPath, true))
                {
                    this.txt_debug_dir.Text = dlg_libdir.SelectedPath;
                }
            }
        }

        private void ChooseReleaseLibFolder(object sender, EventArgs e)
        {
            if (dlg_libdir.ShowDialog() == DialogResult.OK)
            {
                if (this.ValidateLibPath(dlg_libdir.SelectedPath, false))
                {
                    this.txt_release_dir.Text = dlg_libdir.SelectedPath;
                }
            }
        }
        #endregion
        #region control event
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.lbl_debug_torchversion.Text) && !String.IsNullOrWhiteSpace(this.lbl_release_torchversion.Text))
            {
                if (!String.Equals(lbl_debug_torchversion.Text, lbl_release_torchversion.Text)) {
                    MessageBox.Show("Debug version is different from Release version");
                    return;
                }
            }
            if (String.IsNullOrWhiteSpace(this.txt_debug_dir.Text) && String.IsNullOrWhiteSpace(this.txt_release_dir.Text))
            {
                MessageBox.Show("At least one libtorch directory should be selected");
                return;
            }
            if (this.chk_remember.Checked)
            {
                this.SaveSelection();
            }
            else
            {
                this.ClearSelection();
            }

            // use major + minor for exmaple 1.9 for 1.9.0 or 1.9.1, because interface won't change with build number x.x.buildnumber
            var torch_version = this.TorchVersion.Substring(0, this.TorchVersion.Length - 2);
            if (!this.ValidationProjectSettings(torch_version, this.TorchCUDAVersion, this.TorchFullVersion))
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void HoverDir(object sender, EventArgs e)
        {          
            if (sender is TextBox)
            {
                ToolTip tt_dir_name = new ToolTip();
                tt_dir_name.InitialDelay = 0;
                var textBox = (TextBox)sender;
                tt_dir_name.SetToolTip(textBox, textBox.Text);
            }
        }
        #endregion
        private void ValidateUI()
        {

        }

        private bool GetDownloadVersion(string targetDirectory, bool debug)
        {
            var versionFile = Path.Combine(targetDirectory, "libtorch", "build-version");
            if (File.Exists(versionFile))
            {
                if (debug)
                {
                    lbl_debug_torchversion.Text = File.ReadAllText(versionFile);
                    return true;
                }
                else
                {
                    lbl_release_torchversion.Text = File.ReadAllText(versionFile);
                    return true;
                }
            }
            else
            {
                // before 1.9, there's no build-version file, we have to check the version with directory name;
                var idx = targetDirectory.LastIndexOf('-');
                if (idx > 0)
                {
                    var version_str = targetDirectory.Substring(idx + 1);
                    if (debug)
                    {
                        lbl_debug_torchversion.Text = version_str;
                    }
                    else
                    {
                        lbl_release_torchversion.Text = version_str;
                    }
                    return true;
                }
            }
            return false;
        }

        private bool ValidateLibPath(string targetDirectory, bool debug)
        {
            // get libtorch archive: https://github.com/pytorch/pytorch/issues/59607
            if (Directory.Exists(targetDirectory)) {              
                if (Directory.Exists(Path.Combine(targetDirectory, "libtorch"))) 
                {
                    if (debug && !File.Exists(Path.Combine(targetDirectory, "libtorch", "lib", "c10.pdb")))
                    {
                        MessageBox.Show(String.Format("'{0}' doesn't contain a Debug version torchlib", dlg_libdir.SelectedPath)); 
                    }
                    if (!debug && File.Exists(Path.Combine(targetDirectory, "libtorch", "lib", "c10.pdb")))
                    {
                        MessageBox.Show(String.Format("'{0}' doesn't contain a Release version torchlib", dlg_libdir.SelectedPath));
                    }
                    
                    if (this.GetDownloadVersion(targetDirectory, debug))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("build version isn't found, did you rename the directory?");
                    }
                }
                else
                {
                    MessageBox.Show(String.Format("Libtorch isn't in the '{0}'", targetDirectory));
                }
            }
            else
            {
                MessageBox.Show(String.Format("The directory '{0}' doesn't exist", targetDirectory));
            }
            return false;
        }

        private bool ValidationProjectSettings(string torch_version, string torch_cuda_version, string torch_full_version)
        {
            try
            {
                var settingHelper = new TorchVCSettingHelper(torch_version, torch_cuda_version, torch_full_version);

                this.link_options = settingHelper.TorchAdditionalLinkOptions();
                this.torch_debug_libs = settingHelper.TorchDependLibs(this.DebugLibPATH);
                this.torch_release_libs = settingHelper.TorchDependLibs(this.ReleaseLibPATH);
                this.torch_debug_includes = settingHelper.TorchIncludeDirs(this.DebugLibPATH);
                this.torch_release_includes = settingHelper.TorchIncludeDirs(this.ReleaseLibPATH);
            }
            catch (TorchSettingException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        #region settings storage
        private void LoadSelection()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(ConfigForm.regKey))
                {
                    if (key != null)
                    { 
                        var debug_dir = (string)key.GetValue("DEBUG", "");
                        var release_dir = (string)key.GetValue("RELEASE", "");
                        var remember = Convert.ToBoolean(key.GetValue("REMEMBER", "False"));
                        this.txt_debug_dir.Text = debug_dir;
                        this.txt_release_dir.Text = release_dir;
                        this.chk_remember.Checked = remember;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveSelection()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(ConfigForm.regKey))
                {
                    key.SetValue("DEBUG", this.DebugLibPATH);
                    key.SetValue("RELEASE", this.ReleaseLibPATH);
                    key.SetValue("REMEMBER", this.chk_remember.Checked);
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearSelection()
        {
            try
            {
                Registry.CurrentUser.DeleteSubKey(ConfigForm.regKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
