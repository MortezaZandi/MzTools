namespace OLTMockServer.UI
{
    partial class TestContainerControl
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestContainerControl));
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radGridView = new Telerik.WinControls.UI.RadGridView();
            this.radCommandBar1 = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnAddNewOrder = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarSeparator4 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnCreateFastOrder = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarStripElement2 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnRejectOrder = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarSeparator1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnEditOrder = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarSeparator2 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnShowTestOptions = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarSeparator3 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnShowOrderLogs = new Telerik.WinControls.UI.CommandBarButton();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.radGridView);
            this.radPanel1.Controls.Add(this.radCommandBar1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1069, 569);
            this.radPanel1.TabIndex = 0;
            this.radPanel1.Text = "radPanel1";
            this.radPanel1.ThemeName = "Windows7";
            // 
            // radGridView
            // 
            this.radGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView.EnableCustomDrawing = true;
            this.radGridView.Location = new System.Drawing.Point(0, 52);
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
            gridViewTextBoxColumn1.HeaderText = "Code";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.Name = "clmOrderCode";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 90;
            gridViewTextBoxColumn2.FieldName = "UIStatus";
            gridViewTextBoxColumn2.HeaderText = "Status";
            gridViewTextBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn2.Name = "clmOrderStatus";
            gridViewTextBoxColumn2.Width = 120;
            gridViewTextBoxColumn3.DataType = typeof(System.DateTime);
            gridViewTextBoxColumn3.Expression = "";
            gridViewTextBoxColumn3.FieldName = "CreateDate";
            gridViewTextBoxColumn3.FormatString = "{0:yyyy-MM-dd HH:mm:ss}";
            gridViewTextBoxColumn3.HeaderText = "Create Date";
            gridViewTextBoxColumn3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn3.Name = "clmCreateDate";
            gridViewTextBoxColumn3.Width = 120;
            gridViewTextBoxColumn4.FieldName = "VendorCode";
            gridViewTextBoxColumn4.HeaderText = "Vendor Code";
            gridViewTextBoxColumn4.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn4.Name = "clmVendorCode";
            gridViewTextBoxColumn4.Width = 80;
            gridViewTextBoxColumn5.FieldName = "StatusCode";
            gridViewTextBoxColumn5.HeaderText = "Status Code";
            gridViewTextBoxColumn5.Name = "clmStatusCode";
            gridViewTextBoxColumn5.Width = 100;
            gridViewTextBoxColumn6.FieldName = "AckDelay";
            gridViewTextBoxColumn6.HeaderText = "Ack Delay";
            gridViewTextBoxColumn6.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn6.Name = "clmAckDelay";
            gridViewTextBoxColumn6.Width = 100;
            gridViewTextBoxColumn7.FieldName = "PickDelay";
            gridViewTextBoxColumn7.HeaderText = "Pick Delay";
            gridViewTextBoxColumn7.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn7.Name = "clmPickDelay";
            gridViewTextBoxColumn7.Width = 100;
            gridViewTextBoxColumn8.FieldName = "AcceptDelay";
            gridViewTextBoxColumn8.HeaderText = "Accept Delay";
            gridViewTextBoxColumn8.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn8.Name = "clmAcceptDelay";
            gridViewTextBoxColumn8.Width = 100;
            gridViewTextBoxColumn9.FieldName = "Rejected";
            gridViewTextBoxColumn9.HeaderText = "Rejected";
            gridViewTextBoxColumn9.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn9.Name = "clmReject";
            gridViewTextBoxColumn9.Width = 80;
            gridViewTextBoxColumn10.FieldName = "RejectedByVendor";
            gridViewTextBoxColumn10.HeaderText = "Vendor Reject";
            gridViewTextBoxColumn10.Name = "clmVendorReject";
            gridViewTextBoxColumn10.Width = 100;
            gridViewTextBoxColumn11.FieldName = "IsAutoGenerated";
            gridViewTextBoxColumn11.HeaderText = "Auto Gen";
            gridViewTextBoxColumn11.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn11.Name = "clmAutoGenerated";
            gridViewTextBoxColumn11.Width = 80;
            this.radGridView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10,
            gridViewTextBoxColumn11});
            this.radGridView.MasterTemplate.EnableAlternatingRowColor = true;
            this.radGridView.MasterTemplate.EnableFiltering = true;
            this.radGridView.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView.Name = "radGridView";
            this.radGridView.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.radGridView.ShowGroupPanel = false;
            this.radGridView.ShowGroupPanelScrollbars = false;
            this.radGridView.Size = new System.Drawing.Size(1069, 517);
            this.radGridView.TabIndex = 0;
            this.radGridView.ThemeName = "Windows7";
            this.radGridView.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.radGridView_CellFormatting);
            // 
            // radCommandBar1
            // 
            this.radCommandBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radCommandBar1.Location = new System.Drawing.Point(0, 0);
            this.radCommandBar1.Name = "radCommandBar1";
            this.radCommandBar1.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.radCommandBar1.Size = new System.Drawing.Size(1069, 52);
            this.radCommandBar1.TabIndex = 1;
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
            this.commandBarStripElement1.EnableDragging = false;
            this.commandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.btnAddNewOrder,
            this.commandBarSeparator4,
            this.btnCreateFastOrder});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement1.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            this.commandBarStripElement1.StretchHorizontally = false;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement1.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnAddNewOrder
            // 
            this.btnAddNewOrder.ClipDrawing = false;
            this.btnAddNewOrder.ClipText = false;
            this.btnAddNewOrder.DisplayName = "commandBarButton1";
            this.btnAddNewOrder.DrawText = true;
            this.btnAddNewOrder.FlipText = false;
            this.btnAddNewOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNewOrder.Image")));
            this.btnAddNewOrder.Name = "btnAddNewOrder";
            this.btnAddNewOrder.ShouldPaint = true;
            this.btnAddNewOrder.Text = "New Order";
            this.btnAddNewOrder.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddNewOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddNewOrder.TextWrap = true;
            this.btnAddNewOrder.Click += new System.EventHandler(this.btnAddNewOrder_Click);
            // 
            // commandBarSeparator4
            // 
            this.commandBarSeparator4.DisplayName = "commandBarSeparator4";
            this.commandBarSeparator4.Name = "commandBarSeparator4";
            this.commandBarSeparator4.VisibleInOverflowMenu = false;
            // 
            // btnCreateFastOrder
            // 
            this.btnCreateFastOrder.DisplayName = "commandBarButton1";
            this.btnCreateFastOrder.DrawText = true;
            this.btnCreateFastOrder.Image = global::OLTMockServer.Properties.Resources.fast_add_30_1;
            this.btnCreateFastOrder.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnCreateFastOrder.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCreateFastOrder.MaxSize = new System.Drawing.Size(0, 0);
            this.btnCreateFastOrder.Name = "btnCreateFastOrder";
            this.btnCreateFastOrder.Text = "Fast Add";
            this.btnCreateFastOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateFastOrder.Click += new System.EventHandler(this.btnCreateFastOrder_Click);
            // 
            // commandBarStripElement2
            // 
            this.commandBarStripElement2.DisplayName = "commandBarStripElement2";
            this.commandBarStripElement2.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.btnRejectOrder,
            this.commandBarSeparator1,
            this.btnEditOrder,
            this.commandBarSeparator2,
            this.btnShowTestOptions,
            this.commandBarSeparator3,
            this.btnShowOrderLogs});
            this.commandBarStripElement2.Name = "commandBarStripElement2";
            // 
            // 
            // 
            this.commandBarStripElement2.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement2.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnRejectOrder
            // 
            this.btnRejectOrder.DisplayName = "commandBarButton2";
            this.btnRejectOrder.DrawText = true;
            this.btnRejectOrder.Image = global::OLTMockServer.Properties.Resources.reject_24px;
            this.btnRejectOrder.Name = "btnRejectOrder";
            this.btnRejectOrder.Text = "Reject Order";
            this.btnRejectOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRejectOrder.Click += new System.EventHandler(this.btnRejectOrder_Click);
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // btnEditOrder
            // 
            this.btnEditOrder.DisplayName = "commandBarButton3";
            this.btnEditOrder.DrawText = true;
            this.btnEditOrder.Image = global::OLTMockServer.Properties.Resources.edit_24px;
            this.btnEditOrder.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnEditOrder.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditOrder.MaxSize = new System.Drawing.Size(0, 0);
            this.btnEditOrder.Name = "btnEditOrder";
            this.btnEditOrder.Text = "Edit Order";
            this.btnEditOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditOrder.TextWrap = false;
            this.btnEditOrder.Click += new System.EventHandler(this.btnEditOrder_Click);
            // 
            // commandBarSeparator2
            // 
            this.commandBarSeparator2.DisplayName = "commandBarSeparator2";
            this.commandBarSeparator2.Name = "commandBarSeparator2";
            this.commandBarSeparator2.VisibleInOverflowMenu = false;
            // 
            // btnShowTestOptions
            // 
            this.btnShowTestOptions.DisplayName = "commandBarButton4";
            this.btnShowTestOptions.DrawText = true;
            this.btnShowTestOptions.Image = global::OLTMockServer.Properties.Resources.settings_24px;
            this.btnShowTestOptions.Name = "btnShowTestOptions";
            this.btnShowTestOptions.Text = "Test Options";
            this.btnShowTestOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnShowTestOptions.Click += new System.EventHandler(this.btnShowTestOptions_Click);
            // 
            // commandBarSeparator3
            // 
            this.commandBarSeparator3.DisplayName = "commandBarSeparator3";
            this.commandBarSeparator3.Name = "commandBarSeparator3";
            this.commandBarSeparator3.VisibleInOverflowMenu = false;
            // 
            // btnShowOrderLogs
            // 
            this.btnShowOrderLogs.DisplayName = "commandBarButton1";
            this.btnShowOrderLogs.DrawText = true;
            this.btnShowOrderLogs.Image = global::OLTMockServer.Properties.Resources.book_24px;
            this.btnShowOrderLogs.Name = "btnShowOrderLogs";
            this.btnShowOrderLogs.Text = "Order Logs";
            this.btnShowOrderLogs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnShowOrderLogs.Click += new System.EventHandler(this.btnShowOrderLogs_Click);
            // 
            // TestContainerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radPanel1);
            this.Name = "TestContainerControl";
            this.Size = new System.Drawing.Size(1069, 569);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadGridView radGridView;
        private Telerik.WinControls.UI.RadCommandBar radCommandBar1;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.CommandBarButton btnAddNewOrder;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement2;
        private Telerik.WinControls.UI.CommandBarButton btnRejectOrder;
        private Telerik.WinControls.UI.CommandBarButton btnEditOrder;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator2;
        private Telerik.WinControls.UI.CommandBarButton btnShowTestOptions;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator3;
        private Telerik.WinControls.UI.CommandBarButton btnShowOrderLogs;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator4;
        private Telerik.WinControls.UI.CommandBarButton btnCreateFastOrder;
    }
}
