namespace PathShortener
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblTitle = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCopyShortPath = new System.Windows.Forms.Button();
            this.lblChangeStatus = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlOrginalPath = new System.Windows.Forms.Panel();
            this.txtOriginalPath = new System.Windows.Forms.TextBox();
            this.pnlShortPath = new System.Windows.Forms.Panel();
            this.txtShortPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlOrginalPath.SuspendLayout();
            this.pnlShortPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(67)))), ((int)(((byte)(56)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.LavenderBlush;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(10);
            this.lblTitle.Size = new System.Drawing.Size(593, 38);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Path Shortner ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(67)))), ((int)(((byte)(56)))));
            this.linkLabel1.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(558, 5);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(29, 30);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "X";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 37);
            this.label1.TabIndex = 6;
            this.label1.Text = "Original Path";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblChangeStatus);
            this.panel1.Controls.Add(this.btnCopyShortPath);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(593, 155);
            this.panel1.TabIndex = 7;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.pnlShortPath);
            this.panel8.Controls.Add(this.label4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(10, 66);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(573, 37);
            this.panel8.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 37);
            this.label4.TabIndex = 6;
            this.label4.Text = "Short Path";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlOrginalPath);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(573, 37);
            this.panel2.TabIndex = 7;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.btnClear);
            this.panel7.Controls.Add(this.btnOk);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 193);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(593, 42);
            this.panel7.TabIndex = 13;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(489, 6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(92, 26);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Convert";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(573, 19);
            this.panel3.TabIndex = 13;
            // 
            // btnCopyShortPath
            // 
            this.btnCopyShortPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyShortPath.Location = new System.Drawing.Point(488, 113);
            this.btnCopyShortPath.Name = "btnCopyShortPath";
            this.btnCopyShortPath.Size = new System.Drawing.Size(92, 26);
            this.btnCopyShortPath.TabIndex = 0;
            this.btnCopyShortPath.Text = "Copy";
            this.btnCopyShortPath.UseVisualStyleBackColor = true;
            this.btnCopyShortPath.Visible = false;
            this.btnCopyShortPath.Click += new System.EventHandler(this.btnCopyShortPath_Click);
            // 
            // lblChangeStatus
            // 
            this.lblChangeStatus.AutoSize = true;
            this.lblChangeStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeStatus.Location = new System.Drawing.Point(120, 116);
            this.lblChangeStatus.Name = "lblChangeStatus";
            this.lblChangeStatus.Size = new System.Drawing.Size(0, 20);
            this.lblChangeStatus.TabIndex = 15;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Enabled = false;
            this.btnClear.Location = new System.Drawing.Point(391, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(82, 26);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pnlOrginalPath
            // 
            this.pnlOrginalPath.BackColor = System.Drawing.Color.Silver;
            this.pnlOrginalPath.Controls.Add(this.txtOriginalPath);
            this.pnlOrginalPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrginalPath.Location = new System.Drawing.Point(113, 0);
            this.pnlOrginalPath.Name = "pnlOrginalPath";
            this.pnlOrginalPath.Padding = new System.Windows.Forms.Padding(1);
            this.pnlOrginalPath.Size = new System.Drawing.Size(460, 37);
            this.pnlOrginalPath.TabIndex = 7;
            // 
            // txtOriginalPath
            // 
            this.txtOriginalPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOriginalPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOriginalPath.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOriginalPath.Location = new System.Drawing.Point(1, 1);
            this.txtOriginalPath.Multiline = true;
            this.txtOriginalPath.Name = "txtOriginalPath";
            this.txtOriginalPath.Size = new System.Drawing.Size(458, 35);
            this.txtOriginalPath.TabIndex = 1;
            this.txtOriginalPath.TextChanged += new System.EventHandler(this.txtOriginalPath_TextChanged);
            // 
            // pnlShortPath
            // 
            this.pnlShortPath.BackColor = System.Drawing.Color.Silver;
            this.pnlShortPath.Controls.Add(this.txtShortPath);
            this.pnlShortPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlShortPath.Location = new System.Drawing.Point(113, 0);
            this.pnlShortPath.Name = "pnlShortPath";
            this.pnlShortPath.Padding = new System.Windows.Forms.Padding(1);
            this.pnlShortPath.Size = new System.Drawing.Size(460, 37);
            this.pnlShortPath.TabIndex = 7;
            // 
            // txtShortPath
            // 
            this.txtShortPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtShortPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtShortPath.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShortPath.Location = new System.Drawing.Point(1, 1);
            this.txtShortPath.Multiline = true;
            this.txtShortPath.Name = "txtShortPath";
            this.txtShortPath.Size = new System.Drawing.Size(458, 35);
            this.txtShortPath.TabIndex = 1;
            this.txtShortPath.TextChanged += new System.EventHandler(this.txtShortPath_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(10, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Paste your path in original path and press Convert";
            // 
            // Form1
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(593, 235);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lblTitle);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Path Shortner";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.pnlOrginalPath.ResumeLayout(false);
            this.pnlOrginalPath.PerformLayout();
            this.pnlShortPath.ResumeLayout(false);
            this.pnlShortPath.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCopyShortPath;
        private System.Windows.Forms.Label lblChangeStatus;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel pnlShortPath;
        private System.Windows.Forms.TextBox txtShortPath;
        private System.Windows.Forms.Panel pnlOrginalPath;
        private System.Windows.Forms.TextBox txtOriginalPath;
        private System.Windows.Forms.Label label2;
    }
}

