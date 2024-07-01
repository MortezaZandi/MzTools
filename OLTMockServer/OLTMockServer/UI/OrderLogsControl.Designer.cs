namespace OLTMockServer.UI
{
    partial class OrderLogsControl
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
            this.radGridView = new Telerik.WinControls.UI.RadGridView();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.lblAcceptStatus = new Telerik.WinControls.UI.RadLabel();
            this.lblAcceptStatusDescription = new Telerik.WinControls.UI.RadLabel();
            this.lblPickStatus = new Telerik.WinControls.UI.RadLabel();
            this.lblPickStatusDescription = new Telerik.WinControls.UI.RadLabel();
            this.lblACKStatus = new Telerik.WinControls.UI.RadLabel();
            this.lblACKStatusDescription = new Telerik.WinControls.UI.RadLabel();
            this.lblResponseStatus = new Telerik.WinControls.UI.RadLabel();
            this.lblResponseStatusDescription = new Telerik.WinControls.UI.RadLabel();
            this.lblSendStatus = new Telerik.WinControls.UI.RadLabel();
            this.lblSendStatusDescription = new Telerik.WinControls.UI.RadLabel();
            this.radSeparator2 = new Telerik.WinControls.UI.RadSeparator();
            this.radSeparator3 = new Telerik.WinControls.UI.RadSeparator();
            this.radSeparator4 = new Telerik.WinControls.UI.RadSeparator();
            this.radSeparator5 = new Telerik.WinControls.UI.RadSeparator();
            this.radSeparator6 = new Telerik.WinControls.UI.RadSeparator();
            this.radSeparator7 = new Telerik.WinControls.UI.RadSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblAcceptStatus)).BeginInit();
            this.lblAcceptStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblAcceptStatusDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPickStatus)).BeginInit();
            this.lblPickStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblPickStatusDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblACKStatus)).BeginInit();
            this.lblACKStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblACKStatusDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResponseStatus)).BeginInit();
            this.lblResponseStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblResponseStatusDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSendStatus)).BeginInit();
            this.lblSendStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblSendStatusDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator7)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOperations
            // 
            this.pnlOperations.Location = new System.Drawing.Point(0, 534);
            this.pnlOperations.Size = new System.Drawing.Size(1006, 60);
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.radGridView);
            this.radPanel2.Controls.Add(this.radPanel1);
            this.radPanel2.Size = new System.Drawing.Size(1006, 488);
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(1006, 42);
            this.lblTitle.Text = "Order Logs";
            // 
            // radGridView
            // 
            this.radGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView.EnableCustomFiltering = true;
            this.radGridView.Location = new System.Drawing.Point(228, 0);
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
            gridViewTextBoxColumn1.HeaderText = "Log Time";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.Name = "clmLogTime";
            gridViewTextBoxColumn1.Width = 150;
            gridViewTextBoxColumn2.FieldName = "LogDetails";
            gridViewTextBoxColumn2.HeaderText = "Log Details";
            gridViewTextBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn2.Name = "clmLog";
            gridViewTextBoxColumn2.Width = 1000;
            this.radGridView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2});
            this.radGridView.MasterTemplate.EnableCustomFiltering = true;
            this.radGridView.MasterTemplate.ShowRowHeaderColumn = false;
            this.radGridView.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView.Name = "radGridView";
            this.radGridView.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.radGridView.ShowGroupPanel = false;
            this.radGridView.ShowGroupPanelScrollbars = false;
            this.radGridView.Size = new System.Drawing.Size(778, 488);
            this.radGridView.TabIndex = 4;
            this.radGridView.ThemeName = "Windows7";
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.lblAcceptStatus);
            this.radPanel1.Controls.Add(this.lblPickStatus);
            this.radPanel1.Controls.Add(this.lblACKStatus);
            this.radPanel1.Controls.Add(this.lblResponseStatus);
            this.radPanel1.Controls.Add(this.lblSendStatus);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(228, 488);
            this.radPanel1.TabIndex = 5;
            this.radPanel1.Text = "Stats";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radPanel1.ThemeName = "Windows7";
            // 
            // lblAcceptStatus
            // 
            this.lblAcceptStatus.AutoSize = false;
            this.lblAcceptStatus.Controls.Add(this.lblAcceptStatusDescription);
            this.lblAcceptStatus.Controls.Add(this.radSeparator7);
            this.lblAcceptStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAcceptStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcceptStatus.Location = new System.Drawing.Point(0, 320);
            this.lblAcceptStatus.Name = "lblAcceptStatus";
            this.lblAcceptStatus.Size = new System.Drawing.Size(228, 80);
            this.lblAcceptStatus.TabIndex = 4;
            this.lblAcceptStatus.Text = "Accept not received";
            this.lblAcceptStatus.ThemeName = "Windows7";
            // 
            // lblAcceptStatusDescription
            // 
            this.lblAcceptStatusDescription.AutoSize = false;
            this.lblAcceptStatusDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAcceptStatusDescription.Location = new System.Drawing.Point(0, 26);
            this.lblAcceptStatusDescription.Name = "lblAcceptStatusDescription";
            this.lblAcceptStatusDescription.Size = new System.Drawing.Size(228, 50);
            this.lblAcceptStatusDescription.TabIndex = 0;
            this.lblAcceptStatusDescription.Text = "Order sent, But Accept message not received from target vendor";
            // 
            // lblPickStatus
            // 
            this.lblPickStatus.AutoSize = false;
            this.lblPickStatus.Controls.Add(this.lblPickStatusDescription);
            this.lblPickStatus.Controls.Add(this.radSeparator5);
            this.lblPickStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPickStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPickStatus.Location = new System.Drawing.Point(0, 240);
            this.lblPickStatus.Name = "lblPickStatus";
            this.lblPickStatus.Size = new System.Drawing.Size(228, 80);
            this.lblPickStatus.TabIndex = 3;
            this.lblPickStatus.Text = "Pick not received";
            this.lblPickStatus.ThemeName = "Windows7";
            // 
            // lblPickStatusDescription
            // 
            this.lblPickStatusDescription.AutoSize = false;
            this.lblPickStatusDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPickStatusDescription.Location = new System.Drawing.Point(0, 26);
            this.lblPickStatusDescription.Name = "lblPickStatusDescription";
            this.lblPickStatusDescription.Size = new System.Drawing.Size(228, 50);
            this.lblPickStatusDescription.TabIndex = 0;
            this.lblPickStatusDescription.Text = "Order sent, But Pick message not received from target vendor";
            // 
            // lblACKStatus
            // 
            this.lblACKStatus.AutoSize = false;
            this.lblACKStatus.Controls.Add(this.lblACKStatusDescription);
            this.lblACKStatus.Controls.Add(this.radSeparator4);
            this.lblACKStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblACKStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblACKStatus.Location = new System.Drawing.Point(0, 160);
            this.lblACKStatus.Name = "lblACKStatus";
            this.lblACKStatus.Size = new System.Drawing.Size(228, 80);
            this.lblACKStatus.TabIndex = 2;
            this.lblACKStatus.Text = "ACK not received";
            this.lblACKStatus.ThemeName = "Windows7";
            // 
            // lblACKStatusDescription
            // 
            this.lblACKStatusDescription.AutoSize = false;
            this.lblACKStatusDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblACKStatusDescription.Location = new System.Drawing.Point(0, 26);
            this.lblACKStatusDescription.Name = "lblACKStatusDescription";
            this.lblACKStatusDescription.Size = new System.Drawing.Size(228, 50);
            this.lblACKStatusDescription.TabIndex = 0;
            this.lblACKStatusDescription.Text = "Order sent, But Ack message not received from target vendor";
            // 
            // lblResponseStatus
            // 
            this.lblResponseStatus.AutoSize = false;
            this.lblResponseStatus.Controls.Add(this.lblResponseStatusDescription);
            this.lblResponseStatus.Controls.Add(this.radSeparator3);
            this.lblResponseStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResponseStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResponseStatus.Location = new System.Drawing.Point(0, 80);
            this.lblResponseStatus.Name = "lblResponseStatus";
            this.lblResponseStatus.Size = new System.Drawing.Size(228, 80);
            this.lblResponseStatus.TabIndex = 1;
            this.lblResponseStatus.Text = "No Response received";
            this.lblResponseStatus.ThemeName = "Windows7";
            // 
            // lblResponseStatusDescription
            // 
            this.lblResponseStatusDescription.AutoSize = false;
            this.lblResponseStatusDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblResponseStatusDescription.Location = new System.Drawing.Point(0, 26);
            this.lblResponseStatusDescription.Name = "lblResponseStatusDescription";
            this.lblResponseStatusDescription.Size = new System.Drawing.Size(228, 50);
            this.lblResponseStatusDescription.TabIndex = 0;
            this.lblResponseStatusDescription.Text = "The order has been sent, but no response has been received from the vendor.";
            // 
            // lblSendStatus
            // 
            this.lblSendStatus.AutoSize = false;
            this.lblSendStatus.Controls.Add(this.radSeparator6);
            this.lblSendStatus.Controls.Add(this.lblSendStatusDescription);
            this.lblSendStatus.Controls.Add(this.radSeparator2);
            this.lblSendStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSendStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSendStatus.Location = new System.Drawing.Point(0, 0);
            this.lblSendStatus.Name = "lblSendStatus";
            this.lblSendStatus.Size = new System.Drawing.Size(228, 80);
            this.lblSendStatus.TabIndex = 0;
            this.lblSendStatus.Text = "Not send";
            this.lblSendStatus.ThemeName = "Windows7";
            // 
            // lblSendStatusDescription
            // 
            this.lblSendStatusDescription.AutoSize = false;
            this.lblSendStatusDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSendStatusDescription.Location = new System.Drawing.Point(0, 26);
            this.lblSendStatusDescription.Name = "lblSendStatusDescription";
            this.lblSendStatusDescription.Size = new System.Drawing.Size(228, 50);
            this.lblSendStatusDescription.TabIndex = 0;
            this.lblSendStatusDescription.Text = "This order has not yet been sent to the destination vendor.";
            // 
            // radSeparator2
            // 
            this.radSeparator2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radSeparator2.Location = new System.Drawing.Point(0, 76);
            this.radSeparator2.Name = "radSeparator2";
            this.radSeparator2.Size = new System.Drawing.Size(228, 4);
            this.radSeparator2.TabIndex = 1;
            this.radSeparator2.Text = "radSeparator2";
            // 
            // radSeparator3
            // 
            this.radSeparator3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radSeparator3.Location = new System.Drawing.Point(0, 76);
            this.radSeparator3.Name = "radSeparator3";
            this.radSeparator3.Size = new System.Drawing.Size(228, 4);
            this.radSeparator3.TabIndex = 2;
            this.radSeparator3.Text = "radSeparator3";
            // 
            // radSeparator4
            // 
            this.radSeparator4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radSeparator4.Location = new System.Drawing.Point(0, 76);
            this.radSeparator4.Name = "radSeparator4";
            this.radSeparator4.Size = new System.Drawing.Size(228, 4);
            this.radSeparator4.TabIndex = 2;
            this.radSeparator4.Text = "radSeparator4";
            // 
            // radSeparator5
            // 
            this.radSeparator5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radSeparator5.Location = new System.Drawing.Point(0, 76);
            this.radSeparator5.Name = "radSeparator5";
            this.radSeparator5.Size = new System.Drawing.Size(228, 4);
            this.radSeparator5.TabIndex = 2;
            this.radSeparator5.Text = "radSeparator5";
            // 
            // radSeparator6
            // 
            this.radSeparator6.Dock = System.Windows.Forms.DockStyle.Top;
            this.radSeparator6.Location = new System.Drawing.Point(0, 0);
            this.radSeparator6.Name = "radSeparator6";
            this.radSeparator6.Size = new System.Drawing.Size(228, 4);
            this.radSeparator6.TabIndex = 2;
            this.radSeparator6.Text = "radSeparator6";
            // 
            // radSeparator7
            // 
            this.radSeparator7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radSeparator7.Location = new System.Drawing.Point(0, 76);
            this.radSeparator7.Name = "radSeparator7";
            this.radSeparator7.Size = new System.Drawing.Size(228, 4);
            this.radSeparator7.TabIndex = 3;
            this.radSeparator7.Text = "radSeparator7";
            // 
            // OrderLogsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "OrderLogsControl";
            this.Size = new System.Drawing.Size(1006, 594);
            this.Title = "Order Logs";
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblAcceptStatus)).EndInit();
            this.lblAcceptStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblAcceptStatusDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPickStatus)).EndInit();
            this.lblPickStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblPickStatusDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblACKStatus)).EndInit();
            this.lblACKStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblACKStatusDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResponseStatus)).EndInit();
            this.lblResponseStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblResponseStatusDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSendStatus)).EndInit();
            this.lblSendStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblSendStatusDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView radGridView;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel lblSendStatus;
        private Telerik.WinControls.UI.RadLabel lblSendStatusDescription;
        private Telerik.WinControls.UI.RadLabel lblResponseStatus;
        private Telerik.WinControls.UI.RadLabel lblResponseStatusDescription;
        private Telerik.WinControls.UI.RadLabel lblACKStatus;
        private Telerik.WinControls.UI.RadLabel lblACKStatusDescription;
        private Telerik.WinControls.UI.RadLabel lblAcceptStatus;
        private Telerik.WinControls.UI.RadLabel lblAcceptStatusDescription;
        private Telerik.WinControls.UI.RadLabel lblPickStatus;
        private Telerik.WinControls.UI.RadLabel lblPickStatusDescription;
        private Telerik.WinControls.UI.RadSeparator radSeparator5;
        private Telerik.WinControls.UI.RadSeparator radSeparator4;
        private Telerik.WinControls.UI.RadSeparator radSeparator3;
        private Telerik.WinControls.UI.RadSeparator radSeparator2;
        private Telerik.WinControls.UI.RadSeparator radSeparator7;
        private Telerik.WinControls.UI.RadSeparator radSeparator6;
    }
}
