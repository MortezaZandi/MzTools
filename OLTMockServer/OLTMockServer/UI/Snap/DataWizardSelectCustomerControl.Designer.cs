﻿namespace OLTMockServer.UI
{
    partial class DataWizardSelectCustomerControl
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.radGridView = new Telerik.WinControls.UI.RadGridView();
            this.radCommandBar1 = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarSeparator1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarSeparator2 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarSeparator3 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnAddCustomer = new Telerik.WinControls.UI.CommandBarButton();
            this.btnImportFromRMC = new Telerik.WinControls.UI.CommandBarButton();
            this.btnDelete = new Telerik.WinControls.UI.CommandBarButton();
            this.btnDeleteAll = new Telerik.WinControls.UI.CommandBarButton();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.radGridView);
            this.radPanel2.Controls.Add(this.radCommandBar1);
            // 
            // lblTitle
            // 
            this.lblTitle.Text = "Select Customers";
            // 
            // radGridView
            // 
            this.radGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView.EnableCustomFiltering = true;
            this.radGridView.Location = new System.Drawing.Point(0, 67);
            // 
            // 
            // 
            this.radGridView.MasterTemplate.AllowAddNewRow = false;
            this.radGridView.MasterTemplate.AllowColumnChooser = false;
            this.radGridView.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.radGridView.MasterTemplate.AllowColumnReorder = false;
            this.radGridView.MasterTemplate.AllowDeleteRow = false;
            this.radGridView.MasterTemplate.AllowDragToGroup = false;
            this.radGridView.MasterTemplate.AllowEditRow = false;
            this.radGridView.MasterTemplate.AllowRowHeaderContextMenu = false;
            this.radGridView.MasterTemplate.AllowRowResize = false;
            this.radGridView.MasterTemplate.EnableCustomFiltering = true;
            this.radGridView.MasterTemplate.ShowRowHeaderColumn = false;
            this.radGridView.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView.Name = "radGridView";
            this.radGridView.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.radGridView.ShowGroupPanel = false;
            this.radGridView.ShowGroupPanelScrollbars = false;
            this.radGridView.Size = new System.Drawing.Size(690, 261);
            this.radGridView.TabIndex = 2;
            this.radGridView.ThemeName = "Windows7";
            // 
            // radCommandBar1
            // 
            this.radCommandBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radCommandBar1.Location = new System.Drawing.Point(0, 0);
            this.radCommandBar1.Name = "radCommandBar1";
            this.radCommandBar1.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.radCommandBar1.Size = new System.Drawing.Size(690, 67);
            this.radCommandBar1.TabIndex = 3;
            this.radCommandBar1.Text = "radCommandBar1";
            this.radCommandBar1.ThemeName = "Windows7";
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
            this.btnAddCustomer,
            this.commandBarSeparator1,
            this.btnImportFromRMC,
            this.commandBarSeparator2,
            this.btnDelete,
            this.commandBarSeparator3,
            this.btnDeleteAll});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement1.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement1.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.commandBarSeparator1.Text = "";
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // commandBarSeparator2
            // 
            this.commandBarSeparator2.DisplayName = "commandBarSeparator2";
            this.commandBarSeparator2.Name = "commandBarSeparator2";
            this.commandBarSeparator2.Text = "";
            this.commandBarSeparator2.VisibleInOverflowMenu = false;
            // 
            // commandBarSeparator3
            // 
            this.commandBarSeparator3.DisplayName = "commandBarSeparator3";
            this.commandBarSeparator3.Name = "commandBarSeparator3";
            this.commandBarSeparator3.Text = "";
            this.commandBarSeparator3.VisibleInOverflowMenu = false;
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.DisplayName = "commandBarButton1";
            this.btnAddCustomer.DrawText = true;
            this.btnAddCustomer.Image = global::OLTMockServer.Properties.Resources.add_24px;
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Text = "Add New Customer";
            this.btnAddCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // btnImportFromRMC
            // 
            this.btnImportFromRMC.DisplayName = "commandBarButton2";
            this.btnImportFromRMC.DrawText = true;
            this.btnImportFromRMC.Image = global::OLTMockServer.Properties.Resources.import_24px;
            this.btnImportFromRMC.Name = "btnImportFromRMC";
            this.btnImportFromRMC.Text = "Import From RMC";
            this.btnImportFromRMC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayName = "commandBarButton3";
            this.btnDelete.DrawText = true;
            this.btnDelete.Image = global::OLTMockServer.Properties.Resources.delete_24px;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.DisplayName = "commandBarButton4";
            this.btnDeleteAll.DrawText = true;
            this.btnDeleteAll.Image = global::OLTMockServer.Properties.Resources.clean_24px;
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Text = "Delete All";
            this.btnDeleteAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // DataWizardSelectCustomerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "DataWizardSelectCustomerControl";
            this.Title = "Select Customers";
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView radGridView;
        private Telerik.WinControls.UI.RadCommandBar radCommandBar1;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.CommandBarButton btnAddCustomer;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.CommandBarButton btnImportFromRMC;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator2;
        private Telerik.WinControls.UI.CommandBarButton btnDelete;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator3;
        private Telerik.WinControls.UI.CommandBarButton btnDeleteAll;
    }
}