namespace DataCache
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lnkClearLogs = new System.Windows.Forms.LinkLabel();
            this.lnkStartStop = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1085, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 23);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1085, 552);
            this.textBox1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lnkStartStop);
            this.panel1.Controls.Add(this.lnkClearLogs);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 575);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1085, 37);
            this.panel1.TabIndex = 2;
            // 
            // lnkClearLogs
            // 
            this.lnkClearLogs.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lnkClearLogs.AutoSize = true;
            this.lnkClearLogs.Location = new System.Drawing.Point(1042, 15);
            this.lnkClearLogs.Name = "lnkClearLogs";
            this.lnkClearLogs.Size = new System.Drawing.Size(31, 13);
            this.lnkClearLogs.TabIndex = 0;
            this.lnkClearLogs.TabStop = true;
            this.lnkClearLogs.Text = "Clear";
            this.lnkClearLogs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClearLogs_LinkClicked);
            // 
            // lnkStartStop
            // 
            this.lnkStartStop.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkStartStop.AutoSize = true;
            this.lnkStartStop.Location = new System.Drawing.Point(12, 14);
            this.lnkStartStop.Name = "lnkStartStop";
            this.lnkStartStop.Size = new System.Drawing.Size(51, 13);
            this.lnkStartStop.TabIndex = 1;
            this.lnkStartStop.TabStop = true;
            this.lnkStartStop.Text = "Stop logs";
            this.lnkStartStop.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkStartStop_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 612);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lnkClearLogs;
        private System.Windows.Forms.LinkLabel lnkStartStop;
    }
}

