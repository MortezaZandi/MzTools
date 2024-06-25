using Telerik.WinControls.UI;

namespace OLTMockServer.UI
{
    partial class DataWizardBaseControl
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
            this.pnlTitlebar = new Telerik.WinControls.UI.RadPanel();
            this.lblTitle = new Telerik.WinControls.UI.RadLabel();
            this.radSeparator1 = new Telerik.WinControls.UI.RadSeparator();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.pnlOperations = new Telerik.WinControls.UI.RadPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.windows7Theme1 = new Telerik.WinControls.Themes.Windows7Theme();
            this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTitlebar)).BeginInit();
            this.pnlTitlebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).BeginInit();
            this.pnlOperations.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitlebar
            // 
            this.pnlTitlebar.Controls.Add(this.lblTitle);
            this.pnlTitlebar.Controls.Add(this.radSeparator1);
            this.pnlTitlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitlebar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitlebar.Name = "pnlTitlebar";
            this.pnlTitlebar.Size = new System.Drawing.Size(690, 46);
            this.pnlTitlebar.TabIndex = 0;
            this.pnlTitlebar.Text = "radPanel1";
            this.pnlTitlebar.ThemeName = "Windows7";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(690, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Select Items";
            this.lblTitle.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.ThemeName = "Windows7";
            // 
            // radSeparator1
            // 
            this.radSeparator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radSeparator1.Location = new System.Drawing.Point(0, 42);
            this.radSeparator1.Name = "radSeparator1";
            this.radSeparator1.Size = new System.Drawing.Size(690, 4);
            this.radSeparator1.TabIndex = 0;
            this.radSeparator1.Text = "radSeparator1";
            this.radSeparator1.Visible = false;
            // 
            // radPanel2
            // 
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel2.Location = new System.Drawing.Point(0, 46);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(690, 340);
            this.radPanel2.TabIndex = 1;
            this.radPanel2.Text = "radPanel2";
            this.radPanel2.ThemeName = "Windows7";
            // 
            // pnlOperations
            // 
            this.pnlOperations.Controls.Add(this.flowLayoutPanel1);
            this.pnlOperations.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOperations.Location = new System.Drawing.Point(0, 386);
            this.pnlOperations.Name = "pnlOperations";
            this.pnlOperations.Padding = new System.Windows.Forms.Padding(5);
            this.pnlOperations.Size = new System.Drawing.Size(690, 60);
            this.pnlOperations.TabIndex = 2;
            this.pnlOperations.Text = "radPanel3";
            this.pnlOperations.ThemeName = "Windows7";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(680, 50);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // DataWizardBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.pnlOperations);
            this.Controls.Add(this.pnlTitlebar);
            this.Name = "DataWizardBaseControl";
            this.Size = new System.Drawing.Size(690, 446);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTitlebar)).EndInit();
            this.pnlTitlebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).EndInit();
            this.pnlOperations.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel pnlTitlebar;
        protected Telerik.WinControls.UI.RadPanel pnlOperations;
        private Telerik.WinControls.UI.RadSeparator radSeparator1;
        public Telerik.WinControls.UI.RadPanel radPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Telerik.WinControls.Themes.Windows7Theme windows7Theme1;
        private Telerik.WinControls.RadThemeManager radThemeManager1;
        public RadLabel lblTitle;
    }
}
