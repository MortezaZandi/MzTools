namespace OLTMockServer.UI
{
    partial class ItemSelectControl
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
            this.dataWizardSelectItemControl1 = new OLTMockServer.UI.DataWizardSelectItemControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.dataWizardSelectItemControl1);
            // 
            // dataWizardSelectItemControl1
            // 
            this.dataWizardSelectItemControl1.ReadOnly = true;
            this.dataWizardSelectItemControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataWizardSelectItemControl1.Items = null;
            this.dataWizardSelectItemControl1.Location = new System.Drawing.Point(0, 0);
            this.dataWizardSelectItemControl1.Name = "dataWizardSelectItemControl1";
            this.dataWizardSelectItemControl1.ShowOperationCommands = false;
            this.dataWizardSelectItemControl1.ShowTitleBar = false;
            this.dataWizardSelectItemControl1.Size = new System.Drawing.Size(690, 340);
            this.dataWizardSelectItemControl1.TabIndex = 0;
            this.dataWizardSelectItemControl1.Title = "Select Items";
            this.dataWizardSelectItemControl1.UIFactory = null;
            // 
            // ItemSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ItemSelectControl";
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataWizardSelectItemControl dataWizardSelectItemControl1;
    }
}
