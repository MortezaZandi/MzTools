namespace KyanSetup
{
    partial class InstallationTypePageControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rdInstallHeadquarter = new System.Windows.Forms.RadioButton();
            this.rdInstallRetailStore = new System.Windows.Forms.RadioButton();
            this.rdInstallPOS = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCMSPath = new System.Windows.Forms.TextBox();
            this.txtSetupFilePath = new System.Windows.Forms.TextBox();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rdInstallHeadquarter
            // 
            this.rdInstallHeadquarter.AutoSize = true;
            this.rdInstallHeadquarter.Location = new System.Drawing.Point(42, 35);
            this.rdInstallHeadquarter.Name = "rdInstallHeadquarter";
            this.rdInstallHeadquarter.Size = new System.Drawing.Size(128, 18);
            this.rdInstallHeadquarter.TabIndex = 0;
            this.rdInstallHeadquarter.TabStop = true;
            this.rdInstallHeadquarter.Text = "Install Headquarter";
            this.rdInstallHeadquarter.UseVisualStyleBackColor = true;
            // 
            // rdInstallRetailStore
            // 
            this.rdInstallRetailStore.AutoSize = true;
            this.rdInstallRetailStore.Location = new System.Drawing.Point(42, 117);
            this.rdInstallRetailStore.Name = "rdInstallRetailStore";
            this.rdInstallRetailStore.Size = new System.Drawing.Size(123, 18);
            this.rdInstallRetailStore.TabIndex = 1;
            this.rdInstallRetailStore.TabStop = true;
            this.rdInstallRetailStore.Text = "Install Retail Store";
            this.rdInstallRetailStore.UseVisualStyleBackColor = true;
            // 
            // rdInstallPOS
            // 
            this.rdInstallPOS.AutoSize = true;
            this.rdInstallPOS.Location = new System.Drawing.Point(42, 202);
            this.rdInstallPOS.Name = "rdInstallPOS";
            this.rdInstallPOS.Size = new System.Drawing.Size(83, 18);
            this.rdInstallPOS.TabIndex = 2;
            this.rdInstallPOS.TabStop = true;
            this.rdInstallPOS.Text = "Install POS";
            this.rdInstallPOS.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(438, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Use this installation mode if you are installing the first system of your company" +
    ".";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(629, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Use this installation mode if you want to install a retail store by having a dbin" +
    "s file created by MakeCD in company.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(528, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Use this installation mode if you want to install a POS by having a dbins file cr" +
    "eated by MakeCD.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 294);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "Choose new or existing CMS path.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 405);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "MakeCD File Path";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 331);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 14);
            this.label6.TabIndex = 8;
            this.label6.Text = "CMS Path";
            // 
            // txtCMSPath
            // 
            this.txtCMSPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCMSPath.Location = new System.Drawing.Point(163, 330);
            this.txtCMSPath.Name = "txtCMSPath";
            this.txtCMSPath.ReadOnly = true;
            this.txtCMSPath.Size = new System.Drawing.Size(388, 22);
            this.txtCMSPath.TabIndex = 9;
            // 
            // txtSetupFilePath
            // 
            this.txtSetupFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSetupFilePath.Location = new System.Drawing.Point(163, 401);
            this.txtSetupFilePath.Name = "txtSetupFilePath";
            this.txtSetupFilePath.ReadOnly = true;
            this.txtSetupFilePath.Size = new System.Drawing.Size(388, 22);
            this.txtSetupFilePath.TabIndex = 10;
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnUnselectAll.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnUnselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnselectAll.Location = new System.Drawing.Point(557, 330);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(35, 22);
            this.btnUnselectAll.TabIndex = 11;
            this.btnUnselectAll.Text = "...";
            this.btnUnselectAll.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(557, 401);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 22);
            this.button1.TabIndex = 12;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 373);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(201, 14);
            this.label7.TabIndex = 13;
            this.label7.Text = "Select dbIns.zip to create database";
            // 
            // InstallationTypePageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUnselectAll);
            this.Controls.Add(this.txtSetupFilePath);
            this.Controls.Add(this.txtCMSPath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdInstallPOS);
            this.Controls.Add(this.rdInstallRetailStore);
            this.Controls.Add(this.rdInstallHeadquarter);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "InstallationTypePageControl";
            this.Size = new System.Drawing.Size(766, 472);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdInstallHeadquarter;
        private System.Windows.Forms.RadioButton rdInstallRetailStore;
        private System.Windows.Forms.RadioButton rdInstallPOS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCMSPath;
        private System.Windows.Forms.TextBox txtSetupFilePath;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
    }
}
