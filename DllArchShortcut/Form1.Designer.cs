namespace DllArchShortcut
{
    partial class Form1
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
            this.lblDllName = new System.Windows.Forms.Label();
            this.lblDllArc = new System.Windows.Forms.Label();
            this.lblDllPath = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblDllName
            // 
            this.lblDllName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDllName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDllName.Location = new System.Drawing.Point(0, 38);
            this.lblDllName.Name = "lblDllName";
            this.lblDllName.Size = new System.Drawing.Size(405, 47);
            this.lblDllName.TabIndex = 0;
            this.lblDllName.Text = "dll name";
            this.lblDllName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDllArc
            // 
            this.lblDllArc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDllArc.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDllArc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(66)))), ((int)(((byte)(112)))));
            this.lblDllArc.Location = new System.Drawing.Point(0, 124);
            this.lblDllArc.Name = "lblDllArc";
            this.lblDllArc.Size = new System.Drawing.Size(405, 47);
            this.lblDllArc.TabIndex = 2;
            this.lblDllArc.Text = "64 bit";
            this.lblDllArc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDllPath
            // 
            this.lblDllPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDllPath.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDllPath.Location = new System.Drawing.Point(0, 85);
            this.lblDllPath.Name = "lblDllPath";
            this.lblDllPath.Size = new System.Drawing.Size(405, 39);
            this.lblDllPath.TabIndex = 3;
            this.lblDllPath.Text = "dll name";
            this.lblDllPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(66)))), ((int)(((byte)(112)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.LavenderBlush;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(10);
            this.lblTitle.Size = new System.Drawing.Size(405, 38);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Assembly Info";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(66)))), ((int)(((byte)(112)))));
            this.linkLabel1.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(370, 5);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(29, 30);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "X";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(405, 176);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lblDllArc);
            this.Controls.Add(this.lblDllPath);
            this.Controls.Add(this.lblDllName);
            this.Controls.Add(this.lblTitle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assembly Info";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDllName;
        private System.Windows.Forms.Label lblDllArc;
        private System.Windows.Forms.Label lblDllPath;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

