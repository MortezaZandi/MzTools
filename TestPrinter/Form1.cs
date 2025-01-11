using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPrinter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            PopulatePrinters();

            dataGridView1.Rows.Add(new object[] { 141, "ماست موسیر", 25000, 2 });
            dataGridView1.Rows.Add(new object[] { 196, "سر شیر", 50000, 1 });
            dataGridView1.Rows.Add(new object[] { 122, "پنیر خامه", 10000, 4 });
        }

        private void PopulatePrinters()
        {
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                cmbPrinters.Items.Add(printer);
            }

            if (cmbPrinters.Items.Count > 0)
            {
                cmbPrinters.SelectedIndex = 0;
            }
        }

        private void tnSendToPrinter_Click(object sender, EventArgs e)
        {
            Print(false);
        }

        private string GetPrinterName()
        {
            if (cmbPrinters.SelectedItem != null)
            {
                return cmbPrinters.SelectedItem.ToString();
            }
            else
            {
                return null;
            }
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            Print(true);
        }

        private void Print(bool preview)
        {
            try
            {
                FastReport.Report report = new FastReport.Report();
                report.Load("PrintLayout_Receipt_Default.frx");
                var data = GetReportData();
                report.RegisterData(data);
                report.GetDataSource("SaleLineItems").Enabled = true;
                report.SetParameterValue("SaleDocumentId", 4530);
                report.SetParameterValue("SaleDocumentSequnceNumber", 98);
                report.SetParameterValue("OperatorName", "سیما غلامی");
                report.SetParameterValue("ReceiptDate", "1403.10.20");
                report.SetParameterValue("CustomerName", "مهشید تیموری");
                report.SetParameterValue("ReceiptBottomAdsText", "متن تبلیغ پایین رسید");
                report.Prepare();
                var printerName = GetPrinterName();

                if (string.IsNullOrWhiteSpace(printerName))
                {
                    UIHelper.ShowError(new InvalidOperationException("چاپگر یافت نشد"));
                    return;
                }

                report.PrintSettings.Printer = printerName;
                report.PrintSettings.ShowDialog = false;

                if (preview)
                {
                    report.ShowPrepared();
                }
                else
                {
                    report.PrintPrepared();
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex);
            }
        }

        private DataSet GetReportData()
        {
            var data = new DataTable("SaleLineItems");
            data.Columns.Add("ItemId", typeof(int));
            data.Columns.Add("ItemName", typeof(string));
            data.Columns.Add("UnitPrice", typeof(decimal));
            data.Columns.Add("Quantity", typeof(int));

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index < dataGridView1.RowCount - 1)
                {
                    var itemId = (int)row.Cells[0].Value;
                    var itemName = row.Cells[1].Value.ToString();
                    var itemPrice = (int)row.Cells[2].Value;
                    var itemCount = (int)row.Cells[3].Value;

                    data.Rows.Add(new object[] { itemId, itemName, itemPrice, itemCount });
                }
            }

            DataSet ds = new DataSet();

            ds.Tables.Add(data);

            return ds;
        }

    }

    internal class UIHelper
    {
        internal static void ShowError(Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
