namespace LoadAnimationControl
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
            this.loadAnimationControl4 = new howto_point_segment_distance.LoadAnimationControl();
            this.loadAnimationControl3 = new howto_point_segment_distance.LoadAnimationControl();
            this.loadAnimationControl2 = new howto_point_segment_distance.LoadAnimationControl();
            this.loadAnimationControl1 = new howto_point_segment_distance.LoadAnimationControl();
            this.userControl11 = new LoadAnimationControl.UserControl1();
            this.SuspendLayout();
            // 
            // loadAnimationControl4
            // 
            this.loadAnimationControl4.BallColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(84)))), ((int)(((byte)(161)))));
            this.loadAnimationControl4.BallCount = 5;
            this.loadAnimationControl4.Location = new System.Drawing.Point(222, 323);
            this.loadAnimationControl4.Name = "loadAnimationControl4";
            this.loadAnimationControl4.ShowDetails = true;
            this.loadAnimationControl4.Size = new System.Drawing.Size(371, 87);
            this.loadAnimationControl4.TabIndex = 3;
            // 
            // loadAnimationControl3
            // 
            this.loadAnimationControl3.BallColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(84)))), ((int)(((byte)(161)))));
            this.loadAnimationControl3.BallCount = 5;
            this.loadAnimationControl3.Location = new System.Drawing.Point(312, 252);
            this.loadAnimationControl3.Name = "loadAnimationControl3";
            this.loadAnimationControl3.ShowDetails = false;
            this.loadAnimationControl3.Size = new System.Drawing.Size(161, 23);
            this.loadAnimationControl3.TabIndex = 2;
            // 
            // loadAnimationControl2
            // 
            this.loadAnimationControl2.BallColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(84)))), ((int)(((byte)(161)))));
            this.loadAnimationControl2.BallCount = 3;
            this.loadAnimationControl2.Location = new System.Drawing.Point(357, 62);
            this.loadAnimationControl2.Name = "loadAnimationControl2";
            this.loadAnimationControl2.ShowDetails = false;
            this.loadAnimationControl2.Size = new System.Drawing.Size(53, 23);
            this.loadAnimationControl2.TabIndex = 1;
            // 
            // loadAnimationControl1
            // 
            this.loadAnimationControl1.BallColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(84)))), ((int)(((byte)(161)))));
            this.loadAnimationControl1.BallCount = 3;
            this.loadAnimationControl1.Location = new System.Drawing.Point(351, 157);
            this.loadAnimationControl1.Name = "loadAnimationControl1";
            this.loadAnimationControl1.ShowDetails = true;
            this.loadAnimationControl1.Size = new System.Drawing.Size(75, 23);
            this.loadAnimationControl1.TabIndex = 0;
            // 
            // userControl11
            // 
            this.userControl11.Location = new System.Drawing.Point(0, 0);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(607, 150);
            this.userControl11.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.userControl11);
            this.Controls.Add(this.loadAnimationControl4);
            this.Controls.Add(this.loadAnimationControl3);
            this.Controls.Add(this.loadAnimationControl2);
            this.Controls.Add(this.loadAnimationControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private howto_point_segment_distance.LoadAnimationControl loadAnimationControl1;
        private howto_point_segment_distance.LoadAnimationControl loadAnimationControl2;
        private howto_point_segment_distance.LoadAnimationControl loadAnimationControl3;
        private howto_point_segment_distance.LoadAnimationControl loadAnimationControl4;
        private UserControl1 userControl11;
    }
}

