namespace howto_point_segment_distance
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
            this.loadAnimationControl1 = new howto_point_segment_distance.LoadAnimationControl();
            this.SuspendLayout();
            // 
            // loadAnimationControl1
            // 
            this.loadAnimationControl1.BallColor = System.Drawing.Color.Black;
            this.loadAnimationControl1.BallCount = 5;
            this.loadAnimationControl1.Location = new System.Drawing.Point(170, 136);
            this.loadAnimationControl1.Name = "loadAnimationControl1";
            this.loadAnimationControl1.Padding = new System.Windows.Forms.Padding(5);
            this.loadAnimationControl1.Size = new System.Drawing.Size(115, 26);
            this.loadAnimationControl1.TabIndex = 0;
            this.loadAnimationControl1.Text = "loadAnimationControl1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(424, 173);
            this.Controls.Add(this.loadAnimationControl1);
            this.Name = "Form1";
            this.Text = "howto_point_segment_distance";
            this.ResumeLayout(false);

        }

        #endregion

        private LoadAnimationControl loadAnimationControl1;
    }
}

