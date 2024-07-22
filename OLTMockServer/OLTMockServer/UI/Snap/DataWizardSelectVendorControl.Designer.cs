namespace OLTMockServer.UI
{
    partial class DataWizardSelectVendorControl
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.radGridView = new Telerik.WinControls.UI.RadGridView();
            this.radCommandBar1 = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnAddNewVendor = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarSeparator1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarButton1 = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarSeparator2 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnDelete = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarSeparator3 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnDeleteAll = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarSeparator4 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnEditVendor = new Telerik.WinControls.UI.CommandBarButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).BeginInit();
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
            this.lblTitle.Text = "Select Vendors";
            // 
            // radGridView
            // 
            this.radGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView.EnableCustomFiltering = true;
            this.radGridView.Location = new System.Drawing.Point(0, 48);
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
            gridViewTextBoxColumn1.FieldName = "Code";
            gridViewTextBoxColumn1.HeaderText = "Vendor Code";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.Name = "column1";
            gridViewTextBoxColumn1.Width = 150;
            gridViewTextBoxColumn2.FieldName = "Name";
            gridViewTextBoxColumn2.HeaderText = "Name";
            gridViewTextBoxColumn2.Name = "column2";
            gridViewTextBoxColumn2.Width = 150;
            gridViewTextBoxColumn3.FieldName = "BaseUrl";
            gridViewTextBoxColumn3.HeaderText = "Base Url";
            gridViewTextBoxColumn3.Name = "column3";
            gridViewTextBoxColumn3.Width = 300;
            gridViewCheckBoxColumn1.FieldName = "IsActive";
            gridViewCheckBoxColumn1.HeaderText = "Is Active";
            gridViewCheckBoxColumn1.Name = "column4";
            this.radGridView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewCheckBoxColumn1});
            this.radGridView.MasterTemplate.EnableCustomFiltering = true;
            this.radGridView.MasterTemplate.ShowRowHeaderColumn = false;
            this.radGridView.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView.Name = "radGridView";
            this.radGridView.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.radGridView.ShowGroupPanel = false;
            this.radGridView.ShowGroupPanelScrollbars = false;
            this.radGridView.Size = new System.Drawing.Size(690, 292);
            this.radGridView.TabIndex = 3;
            this.radGridView.ThemeName = "Windows7";
            this.radGridView.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.radGridView_CellDoubleClick);
            // 
            // radCommandBar1
            // 
            this.radCommandBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radCommandBar1.Location = new System.Drawing.Point(0, 0);
            this.radCommandBar1.Name = "radCommandBar1";
            this.radCommandBar1.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.radCommandBar1.Size = new System.Drawing.Size(690, 48);
            this.radCommandBar1.TabIndex = 4;
            this.radCommandBar1.Text = "radCommandBar1";
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
            this.btnAddNewVendor,
            this.commandBarSeparator1,
            this.commandBarButton1,
            this.commandBarSeparator2,
            this.btnDelete,
            this.commandBarSeparator3,
            this.btnDeleteAll,
            this.commandBarSeparator4,
            this.btnEditVendor});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement1.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement1.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnAddNewVendor
            // 
            this.btnAddNewVendor.DisplayName = "commandBarButton1";
            this.btnAddNewVendor.DrawText = true;
            this.btnAddNewVendor.Image = global::OLTMockServer.Properties.Resources.add_24px;
            this.btnAddNewVendor.Name = "btnAddNewVendor";
            this.btnAddNewVendor.Text = "Add New Vendor";
            this.btnAddNewVendor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddNewVendor.Click += new System.EventHandler(this.btnAddNewVendor_Click);
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // commandBarButton1
            // 
            this.commandBarButton1.DisplayName = "commandBarButton1";
            this.commandBarButton1.DrawText = true;
            this.commandBarButton1.Image = global::OLTMockServer.Properties.Resources.import_24px;
            this.commandBarButton1.Name = "commandBarButton1";
            this.commandBarButton1.Text = "Import From OLT";
            this.commandBarButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // commandBarSeparator2
            // 
            this.commandBarSeparator2.DisplayName = "commandBarSeparator2";
            this.commandBarSeparator2.Name = "commandBarSeparator2";
            this.commandBarSeparator2.VisibleInOverflowMenu = false;
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayName = "commandBarButton2";
            this.btnDelete.DrawText = true;
            this.btnDelete.Image = global::OLTMockServer.Properties.Resources.delete_24px;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // commandBarSeparator3
            // 
            this.commandBarSeparator3.DisplayName = "commandBarSeparator3";
            this.commandBarSeparator3.Name = "commandBarSeparator3";
            this.commandBarSeparator3.VisibleInOverflowMenu = false;
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.DisplayName = "commandBarButton2";
            this.btnDeleteAll.DrawText = true;
            this.btnDeleteAll.Image = global::OLTMockServer.Properties.Resources.clean_24px;
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Text = "Delete All";
            this.btnDeleteAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // commandBarSeparator4
            // 
            this.commandBarSeparator4.DisplayName = "commandBarSeparator4";
            this.commandBarSeparator4.Name = "commandBarSeparator4";
            this.commandBarSeparator4.VisibleInOverflowMenu = false;
            // 
            // btnEditVendor
            // 
            this.btnEditVendor.DisplayName = "commandBarButton2";
            this.btnEditVendor.DrawText = true;
            this.btnEditVendor.Image = global::OLTMockServer.Properties.Resources.edit_blue_24px;
            this.btnEditVendor.Name = "btnEditVendor";
            this.btnEditVendor.Text = "Edit Vendor";
            this.btnEditVendor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditVendor.Click += new System.EventHandler(this.btnEditVendor_Click);
            // 
            // DataWizardSelectVendorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "DataWizardSelectVendorControl";
            this.Title = "Select Vendors";
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).EndInit();
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
        private Telerik.WinControls.UI.CommandBarButton btnAddNewVendor;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton1;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator2;
        private Telerik.WinControls.UI.CommandBarButton btnDelete;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator3;
        private Telerik.WinControls.UI.CommandBarButton btnDeleteAll;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator4;
        private Telerik.WinControls.UI.CommandBarButton btnEditVendor;
    }
}
