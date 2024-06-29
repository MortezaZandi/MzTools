namespace OLTMockServer.UI
{
    partial class DataWizardSelectOrderPatternControl
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
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn1 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataWizardSelectOrderPatternControl));
            this.radPanel3 = new Telerik.WinControls.UI.RadPanel();
            this.radGridView = new Telerik.WinControls.UI.RadGridView();
            this.radCommandBar1 = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnAddProperty = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarSeparator1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnDeleteProperty = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarSeparator3 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnDeleteAllProperties = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarStripElement2 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarLabel1 = new Telerik.WinControls.UI.CommandBarLabel();
            this.cmbPatterns = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarRowElement2 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.btnApplyPattern = new Telerik.WinControls.UI.CommandBarButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).BeginInit();
            this.radPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOperations
            // 
            this.pnlOperations.Location = new System.Drawing.Point(0, 481);
            this.pnlOperations.Size = new System.Drawing.Size(861, 60);
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.radPanel3);
            this.radPanel2.Size = new System.Drawing.Size(861, 435);
            this.radPanel2.Text = "Define or select order pattern";
            this.radPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(861, 42);
            this.lblTitle.Text = "Select Pattern";
            // 
            // radPanel3
            // 
            this.radPanel3.Controls.Add(this.radGridView);
            this.radPanel3.Controls.Add(this.radCommandBar1);
            this.radPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel3.Location = new System.Drawing.Point(0, 0);
            this.radPanel3.Name = "radPanel3";
            this.radPanel3.Size = new System.Drawing.Size(861, 435);
            this.radPanel3.TabIndex = 1;
            this.radPanel3.Text = "radPanel3";
            // 
            // radGridView
            // 
            this.radGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView.EnableCustomFiltering = true;
            this.radGridView.Location = new System.Drawing.Point(0, 73);
            // 
            // 
            // 
            this.radGridView.MasterTemplate.AllowAddNewRow = false;
            this.radGridView.MasterTemplate.AllowColumnChooser = false;
            this.radGridView.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.radGridView.MasterTemplate.AllowColumnReorder = false;
            this.radGridView.MasterTemplate.AllowDeleteRow = false;
            this.radGridView.MasterTemplate.AllowDragToGroup = false;
            this.radGridView.MasterTemplate.AllowRowHeaderContextMenu = false;
            this.radGridView.MasterTemplate.AllowRowResize = false;
            gridViewTextBoxColumn1.FieldName = "PropertyName";
            gridViewTextBoxColumn1.HeaderText = "Property Name";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.IsPinned = true;
            gridViewTextBoxColumn1.Name = "clmPropertyName";
            gridViewTextBoxColumn1.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 150;
            gridViewTextBoxColumn2.FieldName = "PropertyType";
            gridViewTextBoxColumn2.HeaderText = "Property Type";
            gridViewTextBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn2.IsPinned = true;
            gridViewTextBoxColumn2.Name = "clmProprtyType";
            gridViewTextBoxColumn2.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 100;
            gridViewComboBoxColumn1.FieldName = "GenerateType";
            gridViewComboBoxColumn1.HeaderText = "Generate Type";
            gridViewComboBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewComboBoxColumn1.IsPinned = true;
            gridViewComboBoxColumn1.Name = "clmGeneratorType";
            gridViewComboBoxColumn1.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewComboBoxColumn1.Width = 160;
            gridViewTextBoxColumn3.FieldName = "Value";
            gridViewTextBoxColumn3.HeaderText = "Fixed Value";
            gridViewTextBoxColumn3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn3.IsPinned = true;
            gridViewTextBoxColumn3.Name = "clmFixedValue";
            gridViewTextBoxColumn3.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn3.Width = 150;
            gridViewTextBoxColumn4.FieldName = "SampleValue";
            gridViewTextBoxColumn4.HeaderText = "Sample Value";
            gridViewTextBoxColumn4.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn4.IsPinned = true;
            gridViewTextBoxColumn4.Name = "clmSamplevalue";
            gridViewTextBoxColumn4.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 100;
            gridViewTextBoxColumn5.FieldName = "MinValue";
            gridViewTextBoxColumn5.HeaderText = "MinValue";
            gridViewTextBoxColumn5.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn5.IsPinned = true;
            gridViewTextBoxColumn5.Name = "clmMinValue";
            gridViewTextBoxColumn5.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn5.Width = 80;
            gridViewTextBoxColumn6.FieldName = "MaxValue";
            gridViewTextBoxColumn6.HeaderText = "MaxValue";
            gridViewTextBoxColumn6.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn6.IsPinned = true;
            gridViewTextBoxColumn6.Name = "clmMaxValue";
            gridViewTextBoxColumn6.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn6.Width = 80;
            gridViewCheckBoxColumn1.FieldName = "Unique";
            gridViewCheckBoxColumn1.HeaderText = "Unique";
            gridViewCheckBoxColumn1.Name = "column1";
            this.radGridView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewComboBoxColumn1,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewCheckBoxColumn1});
            this.radGridView.MasterTemplate.EnableCustomFiltering = true;
            this.radGridView.MasterTemplate.ShowRowHeaderColumn = false;
            this.radGridView.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView.Name = "radGridView";
            this.radGridView.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.radGridView.ShowGroupPanel = false;
            this.radGridView.ShowGroupPanelScrollbars = false;
            this.radGridView.Size = new System.Drawing.Size(861, 362);
            this.radGridView.TabIndex = 2;
            this.radGridView.ThemeName = "ControlDefault";
            // 
            // radCommandBar1
            // 
            this.radCommandBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radCommandBar1.Location = new System.Drawing.Point(0, 0);
            this.radCommandBar1.Name = "radCommandBar1";
            this.radCommandBar1.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.radCommandBar1.Size = new System.Drawing.Size(861, 73);
            this.radCommandBar1.TabIndex = 0;
            this.radCommandBar1.Text = "radCommandBar1";
            this.radCommandBar1.ThemeName = "Windows7";
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1,
            this.commandBarStripElement2});
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.btnAddProperty,
            this.commandBarSeparator1,
            this.btnDeleteProperty,
            this.commandBarSeparator3,
            this.btnDeleteAllProperties});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement1.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement1.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnAddProperty
            // 
            this.btnAddProperty.DisplayName = "commandBarButton1";
            this.btnAddProperty.DrawText = true;
            this.btnAddProperty.Image = global::OLTMockServer.Properties.Resources.add_24px;
            this.btnAddProperty.Name = "btnAddProperty";
            this.btnAddProperty.Text = "Add Property";
            this.btnAddProperty.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddProperty.Click += new System.EventHandler(this.btnAddProperty_Click);
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // btnDeleteProperty
            // 
            this.btnDeleteProperty.DisplayName = "commandBarButton2";
            this.btnDeleteProperty.DrawText = true;
            this.btnDeleteProperty.Image = global::OLTMockServer.Properties.Resources.delete_24px;
            this.btnDeleteProperty.Name = "btnDeleteProperty";
            this.btnDeleteProperty.Text = "Delete Property";
            this.btnDeleteProperty.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteProperty.Click += new System.EventHandler(this.btnDeleteProperty_Click);
            // 
            // commandBarSeparator3
            // 
            this.commandBarSeparator3.DisplayName = "commandBarSeparator3";
            this.commandBarSeparator3.Name = "commandBarSeparator3";
            this.commandBarSeparator3.VisibleInOverflowMenu = false;
            // 
            // btnDeleteAllProperties
            // 
            this.btnDeleteAllProperties.DisplayName = "commandBarButton4";
            this.btnDeleteAllProperties.DrawText = true;
            this.btnDeleteAllProperties.Image = global::OLTMockServer.Properties.Resources.clean_24px;
            this.btnDeleteAllProperties.Name = "btnDeleteAllProperties";
            this.btnDeleteAllProperties.Text = "Delete All";
            this.btnDeleteAllProperties.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteAllProperties.Click += new System.EventHandler(this.btnDeleteAllProperties_Click);
            // 
            // commandBarStripElement2
            // 
            this.commandBarStripElement2.AutoSize = false;
            this.commandBarStripElement2.Bounds = new System.Drawing.Rectangle(0, 0, 300, 48);
            this.commandBarStripElement2.DisplayName = "commandBarStripElement2";
            this.commandBarStripElement2.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.commandBarLabel1,
            this.cmbPatterns,
            this.btnApplyPattern});
            this.commandBarStripElement2.Name = "commandBarStripElement2";
            // 
            // 
            // 
            this.commandBarStripElement2.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement2.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // commandBarLabel1
            // 
            this.commandBarLabel1.DisplayName = "commandBarLabel1";
            this.commandBarLabel1.Name = "commandBarLabel1";
            this.commandBarLabel1.Text = "Patterns   ";
            // 
            // cmbPatterns
            // 
            this.cmbPatterns.AutoSize = true;
            this.cmbPatterns.DisplayName = "commandBarDropDownList1";
            this.cmbPatterns.DropDownAnimationEnabled = true;
            this.cmbPatterns.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.cmbPatterns.MaxDropDownItems = 0;
            this.cmbPatterns.MinSize = new System.Drawing.Size(180, 22);
            this.cmbPatterns.Name = "cmbPatterns";
            this.cmbPatterns.Text = "commandBarDropDownList1";
            // 
            // commandBarRowElement2
            // 
            this.commandBarRowElement2.MinSize = new System.Drawing.Size(25, 25);
            // 
            // btnApplyPattern
            // 
            this.btnApplyPattern.DisplayName = "commandBarButton1";
            this.btnApplyPattern.DrawImage = false;
            this.btnApplyPattern.DrawText = true;
            this.btnApplyPattern.Image = ((System.Drawing.Image)(resources.GetObject("btnApplyPattern.Image")));
            this.btnApplyPattern.Name = "btnApplyPattern";
            this.btnApplyPattern.Text = "Apply";
            this.btnApplyPattern.Click += new System.EventHandler(this.btnApplyPattern_Click);
            // 
            // DataWizardSelectOrderPatternControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "DataWizardSelectOrderPatternControl";
            this.Size = new System.Drawing.Size(861, 541);
            this.Title = "Select Pattern";
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).EndInit();
            this.radPanel3.ResumeLayout(false);
            this.radPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel3;
        private Telerik.WinControls.UI.RadCommandBar radCommandBar1;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.RadGridView radGridView;
        private Telerik.WinControls.UI.CommandBarButton btnAddProperty;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.CommandBarButton btnDeleteProperty;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator3;
        private Telerik.WinControls.UI.CommandBarButton btnDeleteAllProperties;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement2;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement2;
        private Telerik.WinControls.UI.CommandBarDropDownList cmbPatterns;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel1;
        private Telerik.WinControls.UI.CommandBarButton btnApplyPattern;
    }
}
