﻿namespace TestPrinter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.roundPanel2 = new TestPrinter.RoundPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.clmItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.roundPanel3 = new TestPrinter.RoundPanel();
            this.btnDeleteRecord = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.tnSendToPrinter = new System.Windows.Forms.Button();
            this.roundPanel1 = new TestPrinter.RoundPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPrinters = new System.Windows.Forms.ComboBox();
            this.roundPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.roundPanel3.SuspendLayout();
            this.roundPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // roundPanel2
            // 
            this.roundPanel2.BackColor = System.Drawing.Color.Gainsboro;
            this.roundPanel2.BorderColor = System.Drawing.Color.DarkGray;
            this.roundPanel2.BorderSize = 0F;
            this.roundPanel2.Controls.Add(this.dataGridView1);
            this.roundPanel2.Controls.Add(this.label2);
            this.roundPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel2.FillColor = System.Drawing.SystemColors.Control;
            this.roundPanel2.Location = new System.Drawing.Point(0, 66);
            this.roundPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.roundPanel2.Name = "roundPanel2";
            this.roundPanel2.Offset = new System.Windows.Forms.Padding(5);
            this.roundPanel2.Radius = 3F;
            this.roundPanel2.Size = new System.Drawing.Size(748, 250);
            this.roundPanel2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmItemId,
            this.clmItemName,
            this.clmUnitPrice,
            this.clmQuantity});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 41);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(748, 209);
            this.dataGridView1.TabIndex = 0;
            // 
            // clmItemId
            // 
            this.clmItemId.HeaderText = "کد کالا";
            this.clmItemId.Name = "clmItemId";
            this.clmItemId.Width = 80;
            // 
            // clmItemName
            // 
            this.clmItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmItemName.HeaderText = "نام کالا";
            this.clmItemName.Name = "clmItemName";
            // 
            // clmUnitPrice
            // 
            this.clmUnitPrice.HeaderText = "قیمت واحد";
            this.clmUnitPrice.Name = "clmUnitPrice";
            // 
            // clmQuantity
            // 
            this.clmQuantity.HeaderText = "تعداد";
            this.clmQuantity.Name = "clmQuantity";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 0, 10, 5);
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(748, 41);
            this.label2.TabIndex = 1;
            this.label2.Text = "لیست کالاها";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // roundPanel3
            // 
            this.roundPanel3.BackColor = System.Drawing.Color.Gainsboro;
            this.roundPanel3.BorderColor = System.Drawing.Color.DarkGray;
            this.roundPanel3.BorderSize = 0F;
            this.roundPanel3.Controls.Add(this.btnDeleteRecord);
            this.roundPanel3.Controls.Add(this.btnPrintPreview);
            this.roundPanel3.Controls.Add(this.tnSendToPrinter);
            this.roundPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.roundPanel3.FillColor = System.Drawing.SystemColors.Control;
            this.roundPanel3.Location = new System.Drawing.Point(0, 316);
            this.roundPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.roundPanel3.Name = "roundPanel3";
            this.roundPanel3.Offset = new System.Windows.Forms.Padding(5);
            this.roundPanel3.Radius = 3F;
            this.roundPanel3.Size = new System.Drawing.Size(748, 69);
            this.roundPanel3.TabIndex = 0;
            // 
            // btnDeleteRecord
            // 
            this.btnDeleteRecord.Location = new System.Drawing.Point(12, 23);
            this.btnDeleteRecord.Name = "btnDeleteRecord";
            this.btnDeleteRecord.Size = new System.Drawing.Size(129, 27);
            this.btnDeleteRecord.TabIndex = 4;
            this.btnDeleteRecord.Text = "حذف رکورد";
            this.btnDeleteRecord.UseVisualStyleBackColor = true;
            this.btnDeleteRecord.Click += new System.EventHandler(this.btnDeleteRecord_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintPreview.Location = new System.Drawing.Point(457, 23);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(129, 27);
            this.btnPrintPreview.TabIndex = 3;
            this.btnPrintPreview.Text = "پیش نمایش";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // tnSendToPrinter
            // 
            this.tnSendToPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tnSendToPrinter.Location = new System.Drawing.Point(592, 23);
            this.tnSendToPrinter.Name = "tnSendToPrinter";
            this.tnSendToPrinter.Size = new System.Drawing.Size(129, 27);
            this.tnSendToPrinter.TabIndex = 0;
            this.tnSendToPrinter.Text = "ارسال به چاپگر";
            this.tnSendToPrinter.UseVisualStyleBackColor = true;
            this.tnSendToPrinter.Click += new System.EventHandler(this.tnSendToPrinter_Click);
            // 
            // roundPanel1
            // 
            this.roundPanel1.BorderColor = System.Drawing.Color.DarkGray;
            this.roundPanel1.BorderSize = 1F;
            this.roundPanel1.Controls.Add(this.pictureBox1);
            this.roundPanel1.Controls.Add(this.label1);
            this.roundPanel1.Controls.Add(this.cmbPrinters);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.roundPanel1.FillColor = System.Drawing.SystemColors.Control;
            this.roundPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Offset = new System.Windows.Forms.Padding(5);
            this.roundPanel1.Radius = 3F;
            this.roundPanel1.Size = new System.Drawing.Size(748, 66);
            this.roundPanel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::TestPrinter.Properties.Resources.icons8_printer_48;
            this.pictureBox1.Location = new System.Drawing.Point(12, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 49);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(661, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(87, 66);
            this.label1.TabIndex = 0;
            this.label1.Text = "انتخاب چاپگر";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbPrinters
            // 
            this.cmbPrinters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinters.FormattingEnabled = true;
            this.cmbPrinters.Location = new System.Drawing.Point(300, 23);
            this.cmbPrinters.Name = "cmbPrinters";
            this.cmbPrinters.Size = new System.Drawing.Size(337, 24);
            this.cmbPrinters.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(748, 385);
            this.Controls.Add(this.roundPanel2);
            this.Controls.Add(this.roundPanel3);
            this.Controls.Add(this.roundPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Printer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.roundPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.roundPanel3.ResumeLayout(false);
            this.roundPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private RoundPanel roundPanel2;
        private RoundPanel roundPanel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQuantity;
        private System.Windows.Forms.Button tnSendToPrinter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPrinters;
        private System.Windows.Forms.Button btnPrintPreview;
        private RoundPanel roundPanel1;
        private System.Windows.Forms.Button btnDeleteRecord;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
    }
}

