namespace TestFontDialog
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
            this.btnChangeFont = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFontName = new System.Windows.Forms.Label();
            this.lblFontFamily = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblFontStyle = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblFontSample = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnChangeFont
            // 
            this.btnChangeFont.Location = new System.Drawing.Point(15, 198);
            this.btnChangeFont.Name = "btnChangeFont";
            this.btnChangeFont.Size = new System.Drawing.Size(75, 23);
            this.btnChangeFont.TabIndex = 0;
            this.btnChangeFont.Text = "Change Font";
            this.btnChangeFont.UseVisualStyleBackColor = true;
            this.btnChangeFont.Click += new System.EventHandler(this.btnChangeFont_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Font Name:";
            // 
            // lblFontName
            // 
            this.lblFontName.AutoSize = true;
            this.lblFontName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFontName.Location = new System.Drawing.Point(91, 9);
            this.lblFontName.Name = "lblFontName";
            this.lblFontName.Size = new System.Drawing.Size(11, 13);
            this.lblFontName.TabIndex = 2;
            this.lblFontName.Text = " ";
            this.lblFontName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFontFamily
            // 
            this.lblFontFamily.AutoSize = true;
            this.lblFontFamily.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFontFamily.Location = new System.Drawing.Point(91, 41);
            this.lblFontFamily.Name = "lblFontFamily";
            this.lblFontFamily.Size = new System.Drawing.Size(11, 13);
            this.lblFontFamily.TabIndex = 4;
            this.lblFontFamily.Text = " ";
            this.lblFontFamily.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Font Family:";
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFontSize.Location = new System.Drawing.Point(91, 75);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(11, 13);
            this.lblFontSize.TabIndex = 6;
            this.lblFontSize.Text = " ";
            this.lblFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Font Size:";
            // 
            // lblFontStyle
            // 
            this.lblFontStyle.AutoSize = true;
            this.lblFontStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFontStyle.Location = new System.Drawing.Point(91, 113);
            this.lblFontStyle.Name = "lblFontStyle";
            this.lblFontStyle.Size = new System.Drawing.Size(11, 13);
            this.lblFontStyle.TabIndex = 8;
            this.lblFontStyle.Text = " ";
            this.lblFontStyle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Font Style:";
            // 
            // lblFontSample
            // 
            this.lblFontSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFontSample.Location = new System.Drawing.Point(287, 21);
            this.lblFontSample.Name = "lblFontSample";
            this.lblFontSample.Size = new System.Drawing.Size(177, 161);
            this.lblFontSample.TabIndex = 9;
            this.lblFontSample.Text = "Sample Text\r\nمتن نمونه";
            this.lblFontSample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 233);
            this.Controls.Add(this.lblFontSample);
            this.Controls.Add(this.lblFontStyle);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblFontSize);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblFontFamily);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblFontName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChangeFont);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChangeFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFontName;
        private System.Windows.Forms.Label lblFontFamily;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblFontStyle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblFontSample;
    }
}

