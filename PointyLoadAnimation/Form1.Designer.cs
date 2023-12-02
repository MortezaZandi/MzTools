namespace PointyLoadAnimation
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
            this.sPanel1 = new PointyLoadAnimation.SPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pointyAnimationControl1 = new PointyLoadAnimation.PointyAnimationControl();
            this.sPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sPanel1
            // 
            this.sPanel1.BackColor = System.Drawing.Color.Silver;
            this.sPanel1.Controls.Add(this.label4);
            this.sPanel1.Controls.Add(this.label3);
            this.sPanel1.Controls.Add(this.label2);
            this.sPanel1.Controls.Add(this.label1);
            this.sPanel1.Controls.Add(this.pointyAnimationControl1);
            this.sPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sPanel1.Location = new System.Drawing.Point(0, 0);
            this.sPanel1.Name = "sPanel1";
            this.sPanel1.Size = new System.Drawing.Size(438, 292);
            this.sPanel1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(187, 144);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(41, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "68%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(152, 102);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(123, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "لطفا منتظر بمانید";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(111, 63);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(217, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "در حال دریافت اطلاعات";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(12, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(394, 1);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // pointyAnimationControl1
            // 
            this.pointyAnimationControl1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pointyAnimationControl1.BackColor = System.Drawing.Color.White;
            this.pointyAnimationControl1.ElementColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pointyAnimationControl1.ElementColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(208)))), ((int)(((byte)(56)))));
            this.pointyAnimationControl1.ElementColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(218)))), ((int)(((byte)(154)))));
            this.pointyAnimationControl1.ElementColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(85)))), ((int)(((byte)(219)))));
            this.pointyAnimationControl1.ElementColor5 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pointyAnimationControl1.ElementsCount = 3;
            this.pointyAnimationControl1.Location = new System.Drawing.Point(170, 197);
            this.pointyAnimationControl1.Name = "pointyAnimationControl1";
            this.pointyAnimationControl1.Size = new System.Drawing.Size(55, 32);
            this.pointyAnimationControl1.Speed = 35;
            this.pointyAnimationControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(438, 292);
            this.Controls.Add(this.sPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Silver;
            this.sPanel1.ResumeLayout(false);
            this.sPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PointyAnimationControl pointyAnimationControl1;
        private SPanel sPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}

