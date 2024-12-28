namespace CodeIDE
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lineNumberedRichTextBox1 = new LineNumberedRichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnFormatCode = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lineNumberedRichTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(74, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(812, 595);
            this.panel1.TabIndex = 0;
            // 
            // lineNumberedRichTextBox1
            // 
            this.lineNumberedRichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lineNumberedRichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineNumberedRichTextBox1.Location = new System.Drawing.Point(0, 0);
            this.lineNumberedRichTextBox1.Name = "lineNumberedRichTextBox1";
            this.lineNumberedRichTextBox1.RightMargin = 500;
            this.lineNumberedRichTextBox1.Size = new System.Drawing.Size(812, 595);
            this.lineNumberedRichTextBox1.TabIndex = 0;
            this.lineNumberedRichTextBox1.Text = "";
            this.lineNumberedRichTextBox1.WordWrap = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnFormatCode);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(74, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(812, 60);
            this.panel2.TabIndex = 0;
            // 
            // btnFormatCode
            // 
            this.btnFormatCode.Location = new System.Drawing.Point(20, 22);
            this.btnFormatCode.Name = "btnFormatCode";
            this.btnFormatCode.Size = new System.Drawing.Size(75, 23);
            this.btnFormatCode.TabIndex = 0;
            this.btnFormatCode.Text = "Format";
            this.btnFormatCode.UseVisualStyleBackColor = true;
            this.btnFormatCode.Click += new System.EventHandler(this.btnFormatCode_Click);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 655);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(886, 34);
            this.panel3.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(886, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 689);
            this.panel4.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(74, 655);
            this.panel5.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 689);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private LineNumberedRichTextBox lineNumberedRichTextBox1;
        private System.Windows.Forms.Button btnFormatCode;
    }
}

