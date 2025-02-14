namespace KyanSetup
{
    partial class MainDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDialog));
            this.pnlPageControl = new KyanSetup.GradientPanel();
            this.homePageControl1 = new KyanSetup.HomePageControl();
            this.pageNavigationList = new KyanSetup.GradientPanel();
            this.gradientPanel4 = new KyanSetup.GradientPanel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gradientPanel1 = new KyanSetup.GradientPanel();
            this.lblPageDescription = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.pnlPageControl.SuspendLayout();
            this.gradientPanel4.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPageControl
            // 
            this.pnlPageControl.BorderColor = System.Drawing.Color.Transparent;
            this.pnlPageControl.Controls.Add(this.homePageControl1);
            this.pnlPageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPageControl.GradientColor1 = System.Drawing.Color.Empty;
            this.pnlPageControl.GradientColor2 = System.Drawing.Color.Empty;
            this.pnlPageControl.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.pnlPageControl.Location = new System.Drawing.Point(244, 77);
            this.pnlPageControl.Name = "pnlPageControl";
            this.pnlPageControl.ShowBottomBorder = false;
            this.pnlPageControl.ShowLeftBorder = false;
            this.pnlPageControl.ShowRightBorder = false;
            this.pnlPageControl.ShowTopBorder = false;
            this.pnlPageControl.Size = new System.Drawing.Size(692, 425);
            this.pnlPageControl.TabIndex = 1;
            // 
            // homePageControl1
            // 
            this.homePageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homePageControl1.Location = new System.Drawing.Point(0, 0);
            this.homePageControl1.Name = "homePageControl1";
            this.homePageControl1.Size = new System.Drawing.Size(692, 425);
            this.homePageControl1.TabIndex = 0;
            // 
            // pageNavigationList
            // 
            this.pageNavigationList.BorderColor = System.Drawing.Color.Silver;
            this.pageNavigationList.Dock = System.Windows.Forms.DockStyle.Left;
            this.pageNavigationList.GradientColor1 = System.Drawing.Color.Empty;
            this.pageNavigationList.GradientColor2 = System.Drawing.Color.Empty;
            this.pageNavigationList.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.pageNavigationList.Location = new System.Drawing.Point(0, 77);
            this.pageNavigationList.Name = "pageNavigationList";
            this.pageNavigationList.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.pageNavigationList.ShowBottomBorder = false;
            this.pageNavigationList.ShowLeftBorder = false;
            this.pageNavigationList.ShowRightBorder = true;
            this.pageNavigationList.ShowTopBorder = false;
            this.pageNavigationList.Size = new System.Drawing.Size(244, 425);
            this.pageNavigationList.TabIndex = 2;
            // 
            // gradientPanel4
            // 
            this.gradientPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.gradientPanel4.BorderColor = System.Drawing.Color.Silver;
            this.gradientPanel4.Controls.Add(this.btnBack);
            this.gradientPanel4.Controls.Add(this.btnNext);
            this.gradientPanel4.Controls.Add(this.btnCancel);
            this.gradientPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gradientPanel4.GradientColor1 = System.Drawing.Color.Transparent;
            this.gradientPanel4.GradientColor2 = System.Drawing.Color.Transparent;
            this.gradientPanel4.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.gradientPanel4.Location = new System.Drawing.Point(0, 502);
            this.gradientPanel4.Name = "gradientPanel4";
            this.gradientPanel4.ShowBottomBorder = false;
            this.gradientPanel4.ShowLeftBorder = false;
            this.gradientPanel4.ShowRightBorder = false;
            this.gradientPanel4.ShowTopBorder = true;
            this.gradientPanel4.Size = new System.Drawing.Size(936, 51);
            this.gradientPanel4.TabIndex = 3;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(634, 15);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(91, 25);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "<Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(731, 15);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(91, 25);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(828, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BorderColor = System.Drawing.Color.Silver;
            this.gradientPanel1.Controls.Add(this.lblPageDescription);
            this.gradientPanel1.Controls.Add(this.lblPageTitle);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.GradientColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.gradientPanel1.GradientColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(208)))), ((int)(((byte)(212)))));
            this.gradientPanel1.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.ShowBottomBorder = true;
            this.gradientPanel1.ShowLeftBorder = false;
            this.gradientPanel1.ShowRightBorder = false;
            this.gradientPanel1.ShowTopBorder = false;
            this.gradientPanel1.Size = new System.Drawing.Size(936, 77);
            this.gradientPanel1.TabIndex = 0;
            // 
            // lblPageDescription
            // 
            this.lblPageDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblPageDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPageDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageDescription.Location = new System.Drawing.Point(0, 40);
            this.lblPageDescription.Name = "lblPageDescription";
            this.lblPageDescription.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.lblPageDescription.Size = new System.Drawing.Size(936, 37);
            this.lblPageDescription.TabIndex = 5;
            this.lblPageDescription.Text = "Create new database or update an existing database to the last version.";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPageTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 0);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblPageTitle.Size = new System.Drawing.Size(936, 40);
            this.lblPageTitle.TabIndex = 4;
            this.lblPageTitle.Text = "Create Database";
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(936, 553);
            this.Controls.Add(this.pnlPageControl);
            this.Controls.Add(this.pageNavigationList);
            this.Controls.Add(this.gradientPanel4);
            this.Controls.Add(this.gradientPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".";
            this.Load += new System.EventHandler(this.MainDialog_Load);
            this.pnlPageControl.ResumeLayout(false);
            this.gradientPanel4.ResumeLayout(false);
            this.gradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GradientPanel gradientPanel1;
        private GradientPanel pnlPageControl;
        private GradientPanel pageNavigationList;
        private GradientPanel gradientPanel4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblPageDescription;
        private System.Windows.Forms.Label lblPageTitle;
        private HomePageControl homePageControl1;
    }
}

