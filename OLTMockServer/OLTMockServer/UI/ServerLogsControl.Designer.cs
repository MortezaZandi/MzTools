namespace OLTMockServer.UI
{
    partial class ServerLogsControl
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.topPanel = new Telerik.WinControls.UI.RadPanel();
            this.btnClearLogs = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarButton1 = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarSeparator1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.chkTopMost = new Telerik.WinControls.UI.CommandBarToggleButton();
            this.backPanel = new Telerik.WinControls.UI.RadPanel();
            this.customGridView1 = new OLTMockServer.UI.CustomGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topPanel)).BeginInit();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOperations
            // 
            this.pnlOperations.Location = new System.Drawing.Point(0, 372);
            this.pnlOperations.Size = new System.Drawing.Size(701, 60);
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.customGridView1);
            this.radPanel2.Location = new System.Drawing.Point(0, 52);
            this.radPanel2.Size = new System.Drawing.Size(701, 320);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.btnClearLogs);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(701, 52);
            this.topPanel.TabIndex = 0;
            // 
            // btnClearLogs
            // 
            this.btnClearLogs.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClearLogs.Location = new System.Drawing.Point(0, 0);
            this.btnClearLogs.Name = "btnClearLogs";
            this.btnClearLogs.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.btnClearLogs.Size = new System.Drawing.Size(701, 48);
            this.btnClearLogs.TabIndex = 0;
            this.btnClearLogs.Text = "radCommandBar1";
            this.btnClearLogs.ThemeName = "Windows7";
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1});
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.commandBarButton1,
            this.commandBarSeparator1,
            this.chkTopMost});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement1.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement1.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // commandBarButton1
            // 
            this.commandBarButton1.DisplayName = "commandBarButton1";
            this.commandBarButton1.DrawText = true;
            this.commandBarButton1.Image = global::OLTMockServer.Properties.Resources.clean_24px;
            this.commandBarButton1.Name = "commandBarButton1";
            this.commandBarButton1.Text = "Clear Logs";
            this.commandBarButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.commandBarButton1.Click += new System.EventHandler(this.commandBarButton1_Click);
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // chkTopMost
            // 
            this.chkTopMost.DisplayName = "commandBarToggleButton1";
            this.chkTopMost.DrawText = true;
            this.chkTopMost.Image = global::OLTMockServer.Properties.Resources.up_24px;
            this.chkTopMost.Name = "chkTopMost";
            this.chkTopMost.Text = "Keep on top";
            this.chkTopMost.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.chkTopMost.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkTopMost_ToggleStateChanged);
            // 
            // backPanel
            // 
            this.backPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backPanel.Location = new System.Drawing.Point(0, 52);
            this.backPanel.Name = "backPanel";
            this.backPanel.Size = new System.Drawing.Size(701, 380);
            this.backPanel.TabIndex = 1;
            // 
            // customGridView1
            // 
            this.customGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridView1.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.customGridView1.MasterTemplate.AllowAddNewRow = false;
            this.customGridView1.MasterTemplate.AllowEditRow = false;
            gridViewTextBoxColumn1.FieldName = "Time";
            gridViewTextBoxColumn1.HeaderText = "Time";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.Name = "column1";
            gridViewTextBoxColumn1.Width = 150;
            gridViewTextBoxColumn2.FieldName = "Log";
            gridViewTextBoxColumn2.HeaderText = "Log";
            gridViewTextBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn2.Name = "column2";
            gridViewTextBoxColumn2.Width = 600;
            this.customGridView1.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2});
            this.customGridView1.MasterTemplate.EnableAlternatingRowColor = true;
            this.customGridView1.MasterTemplate.EnableGrouping = false;
            this.customGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.customGridView1.Name = "customGridView1";
            this.customGridView1.ReadOnly = true;
            this.customGridView1.Size = new System.Drawing.Size(701, 320);
            this.customGridView1.TabIndex = 1;
            this.customGridView1.ThemeName = "Windows7";
            // 
            // ServerLogsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "ServerLogsControl";
            this.ShowTitleBar = false;
            this.Size = new System.Drawing.Size(701, 432);
            this.Controls.SetChildIndex(this.topPanel, 0);
            this.Controls.SetChildIndex(this.backPanel, 0);
            this.Controls.SetChildIndex(this.pnlOperations, 0);
            this.Controls.SetChildIndex(this.radPanel2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topPanel)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel topPanel;
        private Telerik.WinControls.UI.RadPanel backPanel;
        private Telerik.WinControls.UI.RadCommandBar btnClearLogs;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton1;
        private CustomGridView customGridView1;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.CommandBarToggleButton chkTopMost;
    }
}
