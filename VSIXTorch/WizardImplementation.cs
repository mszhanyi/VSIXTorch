namespace VSIXTorch
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TemplateWizard;
    using System.Windows.Forms;
    using EnvDTE;
    using System.IO;
    using System.Diagnostics;
    public class WizardImplementation : IWizard
    {
        // This method is called before opening any item that
        // has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        // This method is only called for item templates,
        // not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem
            projectItem)
        {
        }

        // This method is called after the project is created.
        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            String destinationDirectory = replacementsDictionary["$destinationdirectory$"];
            try
            {
                // Display a form to the user. The form collects
                // input for the custom message.
                var inputForm = new ConfigForm(); 
                var dlgResult = inputForm.ShowDialog();

                if (dlgResult != DialogResult.OK)
                {
                    throw new WizardBackoutException();
                }

                var torch_debug_dir = inputForm.DebugLibPATH;
                var torch_release_dir = inputForm.ReleaseLibPATH;
                var additional_links = inputForm.TorchLinkOptions;
                var torch_debug_include_dirs = inputForm.TorchDebugIncludes;
                var torch_release_include_dirs = inputForm.TorchReleaseIncludes;
                var depend_debug_libs = inputForm.TorchDebugLibs;
                var depend_release_libs = inputForm.TorchReleaseLibs;

                // Add custom parameters.
                replacementsDictionary.Add("$torch_debug_dir$",
                    torch_debug_dir);
                replacementsDictionary.Add("$torch_release_dir$",
                    torch_release_dir);
                replacementsDictionary.Add("$additional_links$",
                    additional_links);
                replacementsDictionary.Add("$torch_debug_include_dirs$",
                    torch_debug_include_dirs);
                replacementsDictionary.Add("$torch_release_include_dirs$",
                    torch_release_include_dirs);
                replacementsDictionary.Add("$depend_debug_libs$",
                    depend_debug_libs);
                replacementsDictionary.Add("$depend_release_libs$",
                    depend_release_libs);
            }
            catch (Exception ex)
            {
                // Clean up the template that was written to disk
                if (Directory.Exists(destinationDirectory))
                {
                    Directory.Delete(destinationDirectory, true);
                }

                Debug.WriteLine(ex);

                throw;
            }
        }

        // This method is only called for item templates,
        // not for project templates.
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }

}