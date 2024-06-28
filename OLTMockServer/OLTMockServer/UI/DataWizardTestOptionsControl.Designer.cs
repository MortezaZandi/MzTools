namespace OLTMockServer.UI
{
    partial class DataWizardTestOptionsControl
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
            this.chkUseRandomDelay = new Telerik.WinControls.UI.RadCheckBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.txtTestName = new Telerik.WinControls.UI.RadTextBoxControl();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.txtMaxOrderCount = new Telerik.WinControls.UI.RadSpinEditor();
            this.chkAutoGenerateOrder = new Telerik.WinControls.UI.RadCheckBox();
            this.pnlDelayAmount = new Telerik.WinControls.UI.RadPanel();
            this.txtMinRandomDelay = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.txtMaxRandomDelay = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.txtDelay = new Telerik.WinControls.UI.RadSpinEditor();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseRandomDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTestName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxOrderCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoGenerateOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDelayAmount)).BeginInit();
            this.pnlDelayAmount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinRandomDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxRandomDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.txtDelay);
            this.radPanel2.Controls.Add(this.pnlDelayAmount);
            this.radPanel2.Controls.Add(this.chkAutoGenerateOrder);
            this.radPanel2.Controls.Add(this.txtMaxOrderCount);
            this.radPanel2.Controls.Add(this.radLabel3);
            this.radPanel2.Controls.Add(this.radLabel2);
            this.radPanel2.Controls.Add(this.txtTestName);
            this.radPanel2.Controls.Add(this.radLabel1);
            this.radPanel2.Controls.Add(this.chkUseRandomDelay);
            this.radPanel2.Text = "";
            // 
            // lblTitle
            // 
            this.lblTitle.Text = "Select Options";
            // 
            // chkUseRandomDelay
            // 
            this.chkUseRandomDelay.Location = new System.Drawing.Point(35, 168);
            this.chkUseRandomDelay.Name = "chkUseRandomDelay";
            this.chkUseRandomDelay.Size = new System.Drawing.Size(246, 16);
            this.chkUseRandomDelay.TabIndex = 0;
            this.chkUseRandomDelay.Text = "Use random delay between send orders (ms)";
            this.chkUseRandomDelay.ThemeName = "Windows7";
            this.chkUseRandomDelay.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkUseRandomDelay_ToggleStateChanged);
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.Location = new System.Drawing.Point(35, 61);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(150, 18);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "Delay between send orders";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radLabel1.ThemeName = "Windows7";
            // 
            // radLabel2
            // 
            this.radLabel2.AutoSize = false;
            this.radLabel2.Location = new System.Drawing.Point(35, 31);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(150, 18);
            this.radLabel2.TabIndex = 3;
            this.radLabel2.Text = "Test Name";
            this.radLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radLabel2.ThemeName = "Windows7";
            // 
            // txtTestName
            // 
            this.txtTestName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTestName.Location = new System.Drawing.Point(191, 29);
            this.txtTestName.Name = "txtTestName";
            this.txtTestName.Size = new System.Drawing.Size(125, 20);
            this.txtTestName.TabIndex = 2;
            this.txtTestName.ThemeName = "Windows7";
            // 
            // radLabel3
            // 
            this.radLabel3.AutoSize = false;
            this.radLabel3.Location = new System.Drawing.Point(35, 94);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(150, 18);
            this.radLabel3.TabIndex = 4;
            this.radLabel3.Text = "Max order count to send";
            this.radLabel3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radLabel3.ThemeName = "Windows7";
            // 
            // txtMaxOrderCount
            // 
            this.txtMaxOrderCount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxOrderCount.Location = new System.Drawing.Point(191, 93);
            this.txtMaxOrderCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtMaxOrderCount.Name = "txtMaxOrderCount";
            this.txtMaxOrderCount.Size = new System.Drawing.Size(125, 23);
            this.txtMaxOrderCount.TabIndex = 5;
            this.txtMaxOrderCount.TabStop = false;
            this.txtMaxOrderCount.ThemeName = "Windows7";
            // 
            // chkAutoGenerateOrder
            // 
            this.chkAutoGenerateOrder.Location = new System.Drawing.Point(35, 132);
            this.chkAutoGenerateOrder.Name = "chkAutoGenerateOrder";
            this.chkAutoGenerateOrder.Size = new System.Drawing.Size(172, 16);
            this.chkAutoGenerateOrder.TabIndex = 6;
            this.chkAutoGenerateOrder.Text = "Generate orders automatically";
            this.chkAutoGenerateOrder.ThemeName = "Windows7";
            this.chkAutoGenerateOrder.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkAutoGenerateOrder_ToggleStateChanged);
            // 
            // pnlDelayAmount
            // 
            this.pnlDelayAmount.Controls.Add(this.txtMaxRandomDelay);
            this.pnlDelayAmount.Controls.Add(this.radLabel5);
            this.pnlDelayAmount.Controls.Add(this.txtMinRandomDelay);
            this.pnlDelayAmount.Controls.Add(this.radLabel4);
            this.pnlDelayAmount.Enabled = false;
            this.pnlDelayAmount.Location = new System.Drawing.Point(35, 190);
            this.pnlDelayAmount.Name = "pnlDelayAmount";
            this.pnlDelayAmount.Size = new System.Drawing.Size(397, 40);
            this.pnlDelayAmount.TabIndex = 7;
            this.pnlDelayAmount.ThemeName = "Windows7";
            // 
            // txtMinRandomDelay
            // 
            this.txtMinRandomDelay.Location = new System.Drawing.Point(69, 10);
            this.txtMinRandomDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtMinRandomDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMinRandomDelay.Name = "txtMinRandomDelay";
            this.txtMinRandomDelay.Size = new System.Drawing.Size(100, 20);
            this.txtMinRandomDelay.TabIndex = 8;
            this.txtMinRandomDelay.TabStop = false;
            this.txtMinRandomDelay.ThemeName = "Windows7";
            this.txtMinRandomDelay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(7, 11);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(56, 18);
            this.radLabel4.TabIndex = 9;
            this.radLabel4.Text = "Min Delay";
            // 
            // txtMaxRandomDelay
            // 
            this.txtMaxRandomDelay.Location = new System.Drawing.Point(253, 10);
            this.txtMaxRandomDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtMaxRandomDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMaxRandomDelay.Name = "txtMaxRandomDelay";
            this.txtMaxRandomDelay.Size = new System.Drawing.Size(100, 20);
            this.txtMaxRandomDelay.TabIndex = 10;
            this.txtMaxRandomDelay.TabStop = false;
            this.txtMaxRandomDelay.ThemeName = "Windows7";
            this.txtMaxRandomDelay.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // radLabel5
            // 
            this.radLabel5.Location = new System.Drawing.Point(191, 11);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(58, 18);
            this.radLabel5.TabIndex = 11;
            this.radLabel5.Text = "Max Delay";
            // 
            // txtDelay
            // 
            this.txtDelay.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDelay.Location = new System.Drawing.Point(191, 60);
            this.txtDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.Size = new System.Drawing.Size(125, 23);
            this.txtDelay.TabIndex = 8;
            this.txtDelay.TabStop = false;
            this.txtDelay.ThemeName = "Windows7";
            this.txtDelay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DataWizardTestOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "DataWizardTestOptionsControl";
            this.Title = "Select Options";
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseRandomDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTestName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxOrderCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoGenerateOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDelayAmount)).EndInit();
            this.pnlDelayAmount.ResumeLayout(false);
            this.pnlDelayAmount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinRandomDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxRandomDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadCheckBox chkUseRandomDelay;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBoxControl txtTestName;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadSpinEditor txtMaxOrderCount;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadCheckBox chkAutoGenerateOrder;
        private Telerik.WinControls.UI.RadPanel pnlDelayAmount;
        private Telerik.WinControls.UI.RadSpinEditor txtMaxRandomDelay;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadSpinEditor txtMinRandomDelay;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadSpinEditor txtDelay;
    }
}
