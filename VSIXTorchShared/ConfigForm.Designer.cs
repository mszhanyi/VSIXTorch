
namespace VSIXTorch
{
    public partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_nvtoolspath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_cudapath = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_remember = new System.Windows.Forms.CheckBox();
            this.lbl_release_torchversion = new System.Windows.Forms.Label();
            this.lbl_debug_torchversion = new System.Windows.Forms.Label();
            this.btn_releaselibfolder = new System.Windows.Forms.Button();
            this.txt_release_dir = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_debuglibfolder = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_debug_dir = new System.Windows.Forms.TextBox();
            this.dlg_libdir = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_nvtoolspath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbl_cudapath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(48, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CUDA Environments";
            // 
            // lbl_nvtoolspath
            // 
            this.lbl_nvtoolspath.AutoSize = true;
            this.lbl_nvtoolspath.Location = new System.Drawing.Point(142, 57);
            this.lbl_nvtoolspath.Name = "lbl_nvtoolspath";
            this.lbl_nvtoolspath.Size = new System.Drawing.Size(68, 13);
            this.lbl_nvtoolspath.TabIndex = 5;
            this.lbl_nvtoolspath.Text = "nvtools_path";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "NVTOOLSEXT_PATH";
            // 
            // lbl_cudapath
            // 
            this.lbl_cudapath.AutoSize = true;
            this.lbl_cudapath.Location = new System.Drawing.Point(107, 28);
            this.lbl_cudapath.Name = "lbl_cudapath";
            this.lbl_cudapath.Size = new System.Drawing.Size(58, 13);
            this.lbl_cudapath.TabIndex = 3;
            this.lbl_cudapath.Text = "cuda_path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "CUDA_PATH";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_remember);
            this.groupBox2.Controls.Add(this.lbl_release_torchversion);
            this.groupBox2.Controls.Add(this.lbl_debug_torchversion);
            this.groupBox2.Controls.Add(this.btn_releaselibfolder);
            this.groupBox2.Controls.Add(this.txt_release_dir);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btn_debuglibfolder);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txt_debug_dir);
            this.groupBox2.Location = new System.Drawing.Point(48, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(468, 198);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LibTorch Directory";
            // 
            // chk_remember
            // 
            this.chk_remember.AutoSize = true;
            this.chk_remember.Location = new System.Drawing.Point(25, 175);
            this.chk_remember.Name = "chk_remember";
            this.chk_remember.Size = new System.Drawing.Size(140, 17);
            this.chk_remember.TabIndex = 8;
            this.chk_remember.Text = "Remember the selection";
            this.chk_remember.UseVisualStyleBackColor = true;
            // 
            // lbl_release_torchversion
            // 
            this.lbl_release_torchversion.AutoSize = true;
            this.lbl_release_torchversion.Location = new System.Drawing.Point(83, 139);
            this.lbl_release_torchversion.Name = "lbl_release_torchversion";
            this.lbl_release_torchversion.Size = new System.Drawing.Size(70, 13);
            this.lbl_release_torchversion.TabIndex = 7;
            this.lbl_release_torchversion.Text = "TorchVersion";
            // 
            // lbl_debug_torchversion
            // 
            this.lbl_debug_torchversion.AutoSize = true;
            this.lbl_debug_torchversion.Location = new System.Drawing.Point(83, 56);
            this.lbl_debug_torchversion.Name = "lbl_debug_torchversion";
            this.lbl_debug_torchversion.Size = new System.Drawing.Size(70, 13);
            this.lbl_debug_torchversion.TabIndex = 6;
            this.lbl_debug_torchversion.Text = "TorchVersion";
            // 
            // btn_releaselibfolder
            // 
            this.btn_releaselibfolder.Location = new System.Drawing.Point(356, 102);
            this.btn_releaselibfolder.Name = "btn_releaselibfolder";
            this.btn_releaselibfolder.Size = new System.Drawing.Size(99, 23);
            this.btn_releaselibfolder.TabIndex = 5;
            this.btn_releaselibfolder.Text = "Select Folder ...";
            this.btn_releaselibfolder.UseVisualStyleBackColor = true;
            this.btn_releaselibfolder.Click += new System.EventHandler(this.ChooseReleaseLibFolder);
            // 
            // txt_release_dir
            // 
            this.txt_release_dir.Location = new System.Drawing.Point(86, 104);
            this.txt_release_dir.Name = "txt_release_dir";
            this.txt_release_dir.Size = new System.Drawing.Size(259, 20);
            this.txt_release_dir.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Release";
            // 
            // btn_debuglibfolder
            // 
            this.btn_debuglibfolder.Location = new System.Drawing.Point(356, 30);
            this.btn_debuglibfolder.Name = "btn_debuglibfolder";
            this.btn_debuglibfolder.Size = new System.Drawing.Size(99, 23);
            this.btn_debuglibfolder.TabIndex = 2;
            this.btn_debuglibfolder.Text = "Select Folder ...";
            this.btn_debuglibfolder.UseVisualStyleBackColor = true;
            this.btn_debuglibfolder.Click += new System.EventHandler(this.ChooseDebugLibFolder);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Debug";
            // 
            // txt_debug_dir
            // 
            this.txt_debug_dir.Location = new System.Drawing.Point(86, 33);
            this.txt_debug_dir.Name = "txt_debug_dir";
            this.txt_debug_dir.Size = new System.Drawing.Size(259, 20);
            this.txt_debug_dir.TabIndex = 0;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(344, 365);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(441, 365);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 400);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_cudapath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_debuglibfolder;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txt_debug_dir;
        private System.Windows.Forms.FolderBrowserDialog dlg_libdir;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_releaselibfolder;
        public System.Windows.Forms.TextBox txt_release_dir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_release_torchversion;
        private System.Windows.Forms.Label lbl_debug_torchversion;
        private System.Windows.Forms.CheckBox chk_remember;
        private System.Windows.Forms.Label lbl_nvtoolspath;
        private System.Windows.Forms.Label label1;
    }
}