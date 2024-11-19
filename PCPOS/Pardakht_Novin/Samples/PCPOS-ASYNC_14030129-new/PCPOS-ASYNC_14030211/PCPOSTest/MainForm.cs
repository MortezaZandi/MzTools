using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Intek.PcPosLibrary;


namespace PCPOSTest
{
    public partial class MainForm : Form
    {
        public int result { get; set; }
       
        private PCPOS pcpos;
        public MainForm()
        {
            InitializeComponent();
            pcpos = new PCPOS();
            pcpos.GetResponse += new PCPOS.ResponseEventHandler(ResponseReady);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            

            //groupBox1.Enabled = false;

            txtAmount2.Enabled = false;
            txtAmount3.Enabled = false;
            txtAmount4.Enabled = false;
            txtAmount5.Enabled = false;
            txtAmount6.Enabled = false;
            txtAmount7.Enabled = false;
            txtAmount8.Enabled = false;
            txtAmount9.Enabled = false;
            txtAmount10.Enabled = false;

            txtID2.Enabled = false;
            txtID3.Enabled = false;
            txtID4.Enabled = false;
            txtID5.Enabled = false;
            txtID6.Enabled = false;
            txtID7.Enabled = false;
            txtID8.Enabled = false;
            txtID9.Enabled = false;
            txtID10.Enabled = false;

            txt_D1.Enabled = false;
            txt_D2.Enabled = false;
            txt_D3.Enabled = false;
            txt_D4.Enabled = false;
            txt_D5.Enabled = false;
            txt_D6.Enabled = false;
            txt_D7.Enabled = false;
            txt_D8.Enabled = false;
            txt_D9.Enabled = false;
            txt_D0.Enabled = false;

            txt_Y1.Enabled = false;
            txt_Y2.Enabled = false;
        }
        private void btn_sendToPos_Click(object sender, EventArgs e) {
            
            if (!checkBoxFaraPurchaseTwoSteps.Checked)//Fara two steps
            {
                pcpos.TerminalID = "";
            }
            txt_resp.Text = "";
            txt_raw_resp.Text = "";
            if (txt_signCode.TextLength >= 3) pcpos.SignCode = txt_signCode.Text;            
            if (chBoxCharge.Checked) {
                if (cmb_Charge.Text != "") {
                         if (cmb_Charge.Text == "شارژ تکی")     pcpos.CH = "02";
                    else if (cmb_Charge.Text == "شارژ گروهی")   pcpos.CH = "01";
                    else if (cmb_Charge.Text == "شارژ مستقیم")  pcpos.CH = "03";
                    else if (cmb_Charge.Text == "شارژ اینترنت") pcpos.CH = "04";
                }
            }            
            pcpos.Amount = txt_amount.Text;
            pcpos.PrCode = txt_prcode.Text;
            if (checkBoxFaraInquery.Checked)//FaraInquery //need ?
            {
                foreach (Control cBox in this.Controls)
                {
                    if (cBox is CheckBox)
                    {
                        ((CheckBox)cBox).Checked = false;
                    }
                }
                //checkBoxMoallem.Checked = false;
                //checkBox1.Checked = false;
                checkBoxKalabarg.Checked = false;
            }
            if (checkBoxMoallem.Checked)//Moallem
            {
                pcpos.MoallemSerial = textBoxMoallemSerial.Text;
                pcpos.MoallemAccount = textBoxMoallemAccount.Text;
            }
            if (checkBoxKalabarg.Checked)//Kalabarg
            {
                pcpos.Kalabarg = textBoxKalaBarg.Text;
            }
            if (checkBox1.Checked) {
                pcpos.BIllID    = txt_BillID.Text;
                pcpos.PaymentID = txt_PaymentID.Text;
            }
            if (chk_params.Checked | chBoxMLTST.Checked) {
                if(getAmount() >= 2000) pcpos.Amount = getAmount().ToString();
                else pcpos.Amount = txt_amount.Text; 

                pcpos.Currency   = txt_currency.Text;
                pcpos.PrCode     = txt_prcode.Text;
                pcpos.R1Holder   = txt_r1.Text;
                pcpos.R3Holder   = txt_r3.Text;
                pcpos.R5Holder   = txt_r5.Text;
                pcpos.R7Holder   = txt_r7.Text;
                pcpos.R9Holder   = txt_r9.Text;
                pcpos.R2Merchant = txt_r2.Text;
                pcpos.R4Merchant = txt_r4.Text;
                pcpos.R6Merchant = txt_r6.Text;
                pcpos.R8Merchant = txt_r8.Text;
                pcpos.R0Merchant = txt_terminal.Text;

                pcpos.T1Holder     = txt_t1.Text;
                pcpos.T2Merchant   = txt_t2.Text;
                pcpos.ServiceGroup = txt_sg.Text;
                if(txt_terminalNumber.TextLength == 8) pcpos.TerminalID = txt_terminalNumber.Text;
                //else MessageBox.Show("Terminal Number must be 8 digits...");
                //--------------------------Amount
                if (txtAmount1.Text.Length > 2 && txtAmount1.Enabled == true)
                    pcpos.Amount1 = txtAmount1.Text;
                if (txtAmount2.Text.Length > 2 && txtAmount2.Enabled == true)                 
                    pcpos.Amount2 = txtAmount2.Text;             
                if (txtAmount3.Text.Length > 2 && txtAmount3.Enabled == true)
                    pcpos.Amount3 = txtAmount3.Text;
                if (txtAmount4.Text.Length > 2 && txtAmount4.Enabled == true)
                    pcpos.Amount4 = txtAmount4.Text;
                if (txtAmount5.Text.Length > 2 && txtAmount5.Enabled == true)
                    pcpos.Amount5 = txtAmount5.Text;
                if (txtAmount6.Text.Length > 2 && txtAmount6.Enabled == true)
                    pcpos.Amount6 = txtAmount6.Text;
                if (txtAmount7.Text.Length > 2 && txtAmount7.Enabled == true)
                    pcpos.Amount7 = txtAmount7.Text;
                if (txtAmount8.Text.Length > 2 && txtAmount8.Enabled == true)
                    pcpos.Amount8 = txtAmount8.Text;
                if (txtAmount9.Text.Length > 2 && txtAmount9.Enabled == true)
                    pcpos.Amount9 = txtAmount9.Text;
                if (txtAmount10.Text.Length > 2 && txtAmount10.Enabled == true)
                    pcpos.Amount10 = txtAmount10.Text;
                //--------------------------IBAN
                if (txtID1.Text.Length > 0 && txtID1.Enabled==true)
                    pcpos.ID1 = txtID1.Text;
                if (txtID2.Text.Length > 0 && txtID2.Enabled == true)
                    pcpos.ID2 = txtID2.Text;
                if (txtID3.Text.Length > 0 && txtID3.Enabled == true)
                    pcpos.ID3 = txtID3.Text;
                if (txtID4.Text.Length > 0 && txtID4.Enabled == true)
                    pcpos.ID4 = txtID4.Text;
                if (txtID5.Text.Length > 0 && txtID5.Enabled == true)
                    pcpos.ID5 = txtID5.Text;
                if (txtID6.Text.Length > 0 && txtID6.Enabled == true)
                    pcpos.ID6 = txtID6.Text;
                if (txtID7.Text.Length > 0 && txtID7.Enabled == true)
                    pcpos.ID7 = txtID7.Text;
                if (txtID8.Text.Length > 0 && txtID8.Enabled == true)
                    pcpos.ID8 = txtID8.Text;
                if (txtID9.Text.Length > 0 && txtID9.Enabled == true)
                    pcpos.ID9 = txtID9.Text;
                if (txtID10.Text.Length > 0 && txtID10.Enabled == true)
                    pcpos.ID10 = txtID10.Text;
                //--------------------------ID
                if (txt_D1.Text.Length > 0 && txt_D1.Enabled == true)
                    pcpos.D1 = txt_D1.Text;
                if (txt_D2.Text.Length > 0 && txt_D2.Enabled == true)
                    pcpos.D2 = txt_D2.Text;
                if (txt_D3.Text.Length > 0 && txt_D3.Enabled == true)
                    pcpos.D3 = txt_D3.Text;
                if (txt_D4.Text.Length > 0 && txt_D4.Enabled == true)
                    pcpos.D4 = txt_D4.Text;
                if (txt_D5.Text.Length > 0 && txt_D5.Enabled == true)
                    pcpos.D5 = txt_D5.Text;
                if (txt_D6.Text.Length > 0 && txt_D6.Enabled == true)
                    pcpos.D6 = txt_D6.Text;
                if (txt_D7.Text.Length > 0 && txt_D7.Enabled == true)
                    pcpos.D7 = txt_D7.Text;
                if (txt_D8.Text.Length > 0 && txt_D8.Enabled == true)
                    pcpos.D8 = txt_D8.Text;
                if (txt_D9.Text.Length > 0 && txt_D9.Enabled == true)
                    pcpos.D9 = txt_D9.Text;
                if (txt_D0.Text.Length > 0 && txt_D0.Enabled == true)
                    pcpos.D10 = txt_D0.Text;
                //ID2
                if (txt_Y1.Text.Length > 0 && txt_Y1.Enabled == true)
                    pcpos.Y1 = txt_Y1.Text;
                if (txt_Y2.Text.Length > 0 && txt_Y2.Enabled == true)
                    pcpos.Y2 = txt_Y2.Text;
            }

            if (rb_lan.Checked) {
                pcpos.ConnectionType = PCPOS.cnType.LAN;
                pcpos.Ip = txtIp.Text;
                pcpos.Port = Int32.Parse(txtPort.Text);
            }
            else {
                string str = cmb_bdRate.Text;
                if (str == "") { MessageBox.Show("Please Select BaudRate!!!"); return; }
                pcpos.baudRate = Int32.Parse(str);
                pcpos.ConnectionType = PCPOS.cnType.SERIAL;                
                pcpos.ComPort = cmb_comport.Text;
            }

            try {                
                if (chBoxCharge.Checked) {
                    if (cmb_Charge.Text != "") pcpos.send_transaction_charge();
                }
                else if (checkBox1.Checked) {
                    if (com_BI_W.Text == "BillPayment") pcpos.send_transaction_bill_payment();
                    if (com_BI_W.Text == "WareHoff"   ) pcpos.send_transaction_WareHoff();
                }
                else if (chBoxMLTST.Checked)
                {
                    if (cmBoxMLTSLT.Text.Equals("MLT")) pcpos.send_transaction_ML();                    
                    else pcpos.send_transaction_SL();
                }
                else if (checkBoxMoallem.Checked)//Moallem
                {
                    result = pcpos.send_transaction_MoallemInsurance();
                    if (result != (int)pna.pcpos.Message.MSG_ID_SUCCESS)
                    {
                        if (result == (int)pna.pcpos.Message.MSG_ID_INVALID)
                            MessageBox.Show(Form.ActiveForm, "شناسه نادرست است !", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        else if (result == (int)pna.pcpos.Message.MSG_ID_EMPTY_BOTH)
                            MessageBox.Show(Form.ActiveForm, "سریال و شماره حساب خالی است !", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        else if (result == (int)pna.pcpos.Message.MSG_ID_EMPTY_SERIAL)
                            MessageBox.Show(Form.ActiveForm, "سریال خالی است !", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        else if (result == (int)pna.pcpos.Message.MSG_ID_EMPTY_ACCOUNT)
                            MessageBox.Show(Form.ActiveForm, "شماره حساب خالی است !", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        else if (result == (int)pna.pcpos.Message.MSG_ID_EMPTY_ACCOUNT)
                            MessageBox.Show(Form.ActiveForm, "شماره حساب خالی است !", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    }
                    else
                    {
                        MessageBox.Show(Form.ActiveForm, "ارسال موفق پیام ", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    }
                }
                else if (checkBoxKalabarg.Checked)//Kalabarg
                {
                    result = pcpos.send_transaction_Kalabarg();
                }
                else if (checkBoxFaraInquery.Checked)//Fara Inquery
                {
                    result = pcpos.send_transaction_FaraInquery();
                    if (result != (int)pna.pcpos.Message.MSG_ID_SUCCESS)
                    {
                        if (result == (int)pna.pcpos.Message.MSG_ID_INVALID_SIGNCODE)
                            MessageBox.Show(Form.ActiveForm, "ساین کد را وارد کنید !", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        else
                            MessageBox.Show(Form.ActiveForm, "مشکلی پیش آمده است", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    }
                    else
                    {
                        MessageBox.Show(Form.ActiveForm, "ارسال موفق پیام ", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    }
                }                
                else if (checkBoxFaraPurchaseTwoSteps.Checked)//Fara two steps
                {
                    result = pcpos.send_transaction_FaraPurchaseTwoSteps();
                    if (result != (int)pna.pcpos.Message.MSG_ID_SUCCESS)
                    {
                        if (result == (int)pna.pcpos.Message.MSG_ID_INVALID_SIGNCODE)
                            MessageBox.Show(Form.ActiveForm, "ساین کد را وارد کنید !", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        else
                            MessageBox.Show(Form.ActiveForm, "مشکلی پیش آمده است", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    }
                    else
                    {
                        MessageBox.Show(Form.ActiveForm, "ارسال موفق پیام ", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    }
                }
                else pcpos.send_transaction();
                txt_req.Text = pcpos.Request;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        void ResponseReady(string raw_response)
        {
            if (txt_raw_resp.InvokeRequired)
            {
                txt_raw_resp.Invoke(new Action<string>(ResponseReady), new object[] { raw_response });
            }
            else
            {
                txt_raw_resp.Text = raw_response;
                txt_resp.Text = pcpos.Response.GetParsedResp(raw_response);
                if (!string.IsNullOrEmpty(pcpos.Response.GetTerminalID(false)))
                {
                    pcpos.TerminalID = pcpos.Response.GetTerminalID(false);
                    textBoxFaraInqueryTerminal.Text = pcpos.TerminalID;

                }
                if (!string.IsNullOrEmpty(pcpos.Response.GetTrxnRRN(false)))
                {
                    pcpos.ReferenceNumber = pcpos.Response.GetTrxnRRN(false);
                    textBoxFaraInqueryRrn.Text = pcpos.ReferenceNumber;
                }

                if (checkBoxFaraInquery.Checked)
                {
                    if (!string.IsNullOrEmpty(pcpos.Response.GetTrxnResp(false)))
                    {
                        if(pcpos.Response.GetTrxnResp(false) == "00")
                        {
                            checkBoxFaraInquery.Checked = false;
                            checkBoxFaraPurchaseTwoSteps.Checked = true;
                        }
                    }
                }
            }
        }
        private void chk_params_CheckedChanged(object sender, EventArgs e)
        {
            gb_params.Enabled = chk_params.Checked;
            chBoxMLTST.Checked = false;
            cmBoxMLTSLT.Text = "";
            chBoxCharge.Checked = false;
            checkBox1.Checked = false;
            checkBoxMoallem.Checked = false;//Moallem
        }
        private void rb_lan_CheckedChanged(object sender, EventArgs e)
        {
            gb_lan.Enabled = rb_lan.Checked;
        }
        private void rd_serial_CheckedChanged(object sender, EventArgs e)
        {            
            gb_serial.Enabled = rb_serial.Checked;            
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
                    
            if (rb_lan.Checked)
            {
                pcpos.ConnectionType = PCPOS.cnType.LAN;
                pcpos.Ip = txtIp.Text;
                pcpos.Port = Int32.Parse(txtPort.Text);
            }
            else
            {
                string str = cmb_bdRate.Text;
                if (str == "") { MessageBox.Show("Please Select BaudRate!!!"); return; }

                pcpos.ConnectionType = PCPOS.cnType.SERIAL;
                pcpos.baudRate = Int32.Parse(str);
                pcpos.ComPort = cmb_comport.Text;
            }

            if(pcpos.TestConnection())
            {
                MessageBox.Show("Connection is successful");
            }
            else
            {                
                MessageBox.Show("No Connection");
            }

        }        
        public int getAmount()
        {
            try
            {
                int amount = 0;
                int total = Convert.ToInt32(txt_total.Text);

                if (total == 1)
                    return amount = Convert.ToInt32(txtAmount1.Text);
                if (total == 2)
                    return amount = Convert.ToInt32(txtAmount1.Text) + Convert.ToInt32(txtAmount2.Text);
                if (total == 3)
                    return amount = Convert.ToInt32(txtAmount1.Text) + Convert.ToInt32(txtAmount2.Text) + Convert.ToInt32(txtAmount3.Text);
                if (total == 4)
                    return amount = Convert.ToInt32(txtAmount1.Text) + Convert.ToInt32(txtAmount2.Text) + Convert.ToInt32(txtAmount3.Text) + Convert.ToInt32(txtAmount4.Text);
                if (total == 5)
                    return amount = Convert.ToInt32(txtAmount1.Text) + Convert.ToInt32(txtAmount2.Text) + Convert.ToInt32(txtAmount3.Text) + Convert.ToInt32(txtAmount4.Text) + Convert.ToInt32(txtAmount5.Text);
                if (total == 6)
                    return amount = Convert.ToInt32(txtAmount1.Text) + Convert.ToInt32(txtAmount2.Text) + Convert.ToInt32(txtAmount3.Text) + Convert.ToInt32(txtAmount4.Text) + Convert.ToInt32(txtAmount5.Text) + Convert.ToInt32(txtAmount6.Text);
                if (total == 7)
                    return amount = Convert.ToInt32(txtAmount1.Text) + Convert.ToInt32(txtAmount2.Text) + Convert.ToInt32(txtAmount3.Text) + Convert.ToInt32(txtAmount4.Text) + Convert.ToInt32(txtAmount5.Text) + Convert.ToInt32(txtAmount6.Text) + Convert.ToInt32(txtAmount7.Text);
                if (total == 8)
                    return amount = Convert.ToInt32(txtAmount1.Text) + Convert.ToInt32(txtAmount2.Text) + Convert.ToInt32(txtAmount3.Text) + Convert.ToInt32(txtAmount4.Text) + Convert.ToInt32(txtAmount5.Text) + Convert.ToInt32(txtAmount6.Text) + Convert.ToInt32(txtAmount7.Text) + Convert.ToInt32(txtAmount8.Text);
                if (total == 9)
                    return amount = Convert.ToInt32(txtAmount1.Text) + Convert.ToInt32(txtAmount2.Text) + Convert.ToInt32(txtAmount3.Text) + Convert.ToInt32(txtAmount4.Text) + Convert.ToInt32(txtAmount5.Text) + Convert.ToInt32(txtAmount6.Text) + Convert.ToInt32(txtAmount7.Text) + Convert.ToInt32(txtAmount8.Text) + Convert.ToInt32(txtAmount9.Text);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return 0;
            }
            return 0;
        }        
        private void txt_total_TextChanged(object sender, EventArgs e)
        {
            if (txt_total.Text == "")
                txt_total.Text = "0";
            int total_amount = Convert.ToInt32(txt_total.Text);
            if (total_amount >= 0 && total_amount <= 10)
            {
                switch (total_amount)
                {
                    case 1:
                        txtAmount2.Enabled = false;
                        txtAmount3.Enabled = false;
                        txtAmount4.Enabled = false;
                        txtAmount5.Enabled = false;
                        txtAmount6.Enabled = false;
                        txtAmount7.Enabled = false;
                        txtAmount8.Enabled = false;
                        txtAmount9.Enabled = false;
                        txtAmount10.Enabled = false;
                        txtAmount1.Enabled = true;

                        if(!chBoxMLTST.Checked)
                        {
                            txtID1.Enabled = true;
                            txtID2.Enabled = false;
                            txtID3.Enabled = false;
                            txtID4.Enabled = false;
                            txtID5.Enabled = false;
                            txtID6.Enabled = false;
                            txtID7.Enabled = false;
                            txtID8.Enabled = false;
                            txtID9.Enabled = false;
                            txtID10.Enabled = false;
                        }
                        

                        txt_D1.Enabled = true;
                        txt_D2.Enabled = false;
                        txt_D3.Enabled = false;
                        txt_D4.Enabled = false;
                        txt_D5.Enabled = false;
                        txt_D6.Enabled = false;
                        txt_D7.Enabled = false;
                        txt_D8.Enabled = false;
                        txt_D9.Enabled = false;
                        txt_D0.Enabled = false;


                        txt_Y1.Enabled = true;
                        txt_Y2.Enabled = false;
                        break;
                    case 2:
                        txtAmount3.Enabled = false;
                        txtAmount4.Enabled = false;
                        txtAmount5.Enabled = false;
                        txtAmount6.Enabled = false;
                        txtAmount7.Enabled = false;
                        txtAmount8.Enabled = false;
                        txtAmount9.Enabled = false;
                        txtAmount10.Enabled = false;
                        txtAmount1.Enabled = true;
                        txtAmount2.Enabled = true;

                        if (!chBoxMLTST.Checked)
                        {
                            txtID1.Enabled = true;
                            txtID2.Enabled = true;
                            txtID3.Enabled = false;
                            txtID4.Enabled = false;
                            txtID5.Enabled = false;
                            txtID6.Enabled = false;
                            txtID7.Enabled = false;
                            txtID8.Enabled = false;
                            txtID9.Enabled = false;
                            txtID10.Enabled = false;
                        }
                        

                        txt_D1.Enabled = true;
                        txt_D2.Enabled = true;
                        txt_D3.Enabled = false;
                        txt_D4.Enabled = false;
                        txt_D5.Enabled = false;
                        txt_D6.Enabled = false;
                        txt_D7.Enabled = false;
                        txt_D8.Enabled = false;
                        txt_D9.Enabled = false;
                        txt_D0.Enabled = false;

                        txt_Y1.Enabled = true;
                        txt_Y2.Enabled = true;

                        break;
                    case 3:
                        txtAmount4.Enabled = false;
                        txtAmount5.Enabled = false;
                        txtAmount6.Enabled = false;
                        txtAmount7.Enabled = false;
                        txtAmount8.Enabled = false;
                        txtAmount9.Enabled = false;
                        txtAmount10.Enabled = false;
                        txtAmount1.Enabled = true;
                        txtAmount2.Enabled = true;
                        txtAmount3.Enabled = true;

                        if (!chBoxMLTST.Checked)
                        {
                            txtID1.Enabled = true;
                            txtID2.Enabled = true;
                            txtID3.Enabled = true;
                            txtID4.Enabled = false;
                            txtID5.Enabled = false;
                            txtID6.Enabled = false;
                            txtID7.Enabled = false;
                            txtID8.Enabled = false;
                            txtID9.Enabled = false;
                            txtID10.Enabled = false;
                        }
                        

                        txt_D1.Enabled = true;
                        txt_D2.Enabled = true;
                        txt_D3.Enabled = true;
                        txt_D4.Enabled = false;
                        txt_D5.Enabled = false;
                        txt_D6.Enabled = false;
                        txt_D7.Enabled = false;
                        txt_D8.Enabled = false;
                        txt_D9.Enabled = false;
                        txt_D0.Enabled = false;

                        break;
                    case 4:
                        txtAmount5.Enabled = false;
                        txtAmount6.Enabled = false;
                        txtAmount7.Enabled = false;
                        txtAmount8.Enabled = false;
                        txtAmount9.Enabled = false;
                        txtAmount10.Enabled = false;
                        txtAmount1.Enabled = true;
                        txtAmount2.Enabled = true;
                        txtAmount3.Enabled = true;
                        txtAmount4.Enabled = true;

                        if (!chBoxMLTST.Checked)
                        {
                            txtID1.Enabled = true;
                            txtID2.Enabled = true;
                            txtID3.Enabled = true;
                            txtID4.Enabled = true;
                            txtID5.Enabled = false;
                            txtID6.Enabled = false;
                            txtID7.Enabled = false;
                            txtID8.Enabled = false;
                            txtID9.Enabled = false;
                            txtID10.Enabled = false;
                        }
                        

                        txt_D1.Enabled = true;
                        txt_D2.Enabled = true;
                        txt_D3.Enabled = true;
                        txt_D4.Enabled = true;
                        txt_D5.Enabled = false;
                        txt_D6.Enabled = false;
                        txt_D7.Enabled = false;
                        txt_D8.Enabled = false;
                        txt_D9.Enabled = false;
                        txt_D0.Enabled = false;

                        break;
                    case 5:
                        txtAmount6.Enabled = false;
                        txtAmount7.Enabled = false;
                        txtAmount8.Enabled = false;
                        txtAmount9.Enabled = false;
                        txtAmount10.Enabled = false;
                        txtAmount1.Enabled = true;
                        txtAmount2.Enabled = true;
                        txtAmount3.Enabled = true;
                        txtAmount4.Enabled = true;
                        txtAmount5.Enabled = true;

                        if (!chBoxMLTST.Checked)
                        {
                            txtID1.Enabled = true;
                            txtID2.Enabled = true;
                            txtID3.Enabled = true;
                            txtID4.Enabled = true;
                            txtID5.Enabled = true;
                            txtID6.Enabled = false;
                            txtID7.Enabled = false;
                            txtID8.Enabled = false;
                            txtID9.Enabled = false;
                            txtID10.Enabled = false;
                        }
                        

                        txt_D1.Enabled = true;
                        txt_D2.Enabled = true;
                        txt_D3.Enabled = true;
                        txt_D4.Enabled = true;
                        txt_D5.Enabled = true;
                        txt_D6.Enabled = false;
                        txt_D7.Enabled = false;
                        txt_D8.Enabled = false;
                        txt_D9.Enabled = false;
                        txt_D0.Enabled = false;

                        break;
                    case 6:
                        txtAmount7.Enabled = false;
                        txtAmount8.Enabled = false;
                        txtAmount9.Enabled = false;
                        txtAmount10.Enabled = false;
                        txtAmount1.Enabled = true;
                        txtAmount2.Enabled = true;
                        txtAmount3.Enabled = true;
                        txtAmount4.Enabled = true;
                        txtAmount5.Enabled = true;
                        txtAmount6.Enabled = true;

                        if (!chBoxMLTST.Checked)
                        {
                            txtID1.Enabled = true;
                            txtID2.Enabled = true;
                            txtID3.Enabled = true;
                            txtID4.Enabled = true;
                            txtID5.Enabled = true;
                            txtID6.Enabled = true;
                            txtID7.Enabled = false;
                            txtID8.Enabled = false;
                            txtID9.Enabled = false;
                            txtID10.Enabled = false;
                        }
                        

                        txt_D1.Enabled = true;
                        txt_D2.Enabled = true;
                        txt_D3.Enabled = true;
                        txt_D4.Enabled = true;
                        txt_D5.Enabled = true;
                        txt_D6.Enabled = true;
                        txt_D7.Enabled = false;
                        txt_D8.Enabled = false;
                        txt_D9.Enabled = false;
                        txt_D0.Enabled = false;

                        break;
                    case 7:
                        txtAmount8.Enabled = false;
                        txtAmount9.Enabled = false;
                        txtAmount10.Enabled = false;
                        txtAmount1.Enabled = true;
                        txtAmount2.Enabled = true;
                        txtAmount3.Enabled = true;
                        txtAmount4.Enabled = true;
                        txtAmount5.Enabled = true;
                        txtAmount6.Enabled = true;
                        txtAmount7.Enabled = true;

                        if (!chBoxMLTST.Checked)
                        {
                            txtID1.Enabled = true;
                            txtID2.Enabled = true;
                            txtID3.Enabled = true;
                            txtID4.Enabled = true;
                            txtID5.Enabled = true;
                            txtID6.Enabled = true;
                            txtID7.Enabled = true;
                            txtID8.Enabled = false;
                            txtID9.Enabled = false;
                            txtID10.Enabled = false;
                        }
                        

                        txt_D1.Enabled = true;
                        txt_D2.Enabled = true;
                        txt_D3.Enabled = true;
                        txt_D4.Enabled = true;
                        txt_D5.Enabled = true;
                        txt_D6.Enabled = true;
                        txt_D7.Enabled = true;
                        txt_D8.Enabled = false;
                        txt_D9.Enabled = false;
                        txt_D0.Enabled = false;

                        break;
                    case 8:
                        txtAmount9.Enabled = false;
                        txtAmount10.Enabled = false;
                        txtAmount1.Enabled = true;
                        txtAmount2.Enabled = true;
                        txtAmount3.Enabled = true;
                        txtAmount4.Enabled = true;
                        txtAmount5.Enabled = true;
                        txtAmount6.Enabled = true;
                        txtAmount7.Enabled = true;
                        txtAmount8.Enabled = true;

                        if (!chBoxMLTST.Checked)
                        {
                            txtID1.Enabled = true;
                            txtID2.Enabled = true;
                            txtID3.Enabled = true;
                            txtID4.Enabled = true;
                            txtID5.Enabled = true;
                            txtID6.Enabled = true;
                            txtID7.Enabled = true;
                            txtID8.Enabled = true;
                            txtID9.Enabled = false;
                            txtID10.Enabled = false;
                        }
                        

                        txt_D1.Enabled = true;
                        txt_D2.Enabled = true;
                        txt_D3.Enabled = true;
                        txt_D4.Enabled = true;
                        txt_D5.Enabled = true;
                        txt_D6.Enabled = true;
                        txt_D7.Enabled = true;
                        txt_D8.Enabled = true;
                        txt_D9.Enabled = false;
                        txt_D0.Enabled = false;

                        break;
                    case 9:
                        txtAmount10.Enabled = false;
                        txtAmount1.Enabled = true;
                        txtAmount2.Enabled = true;
                        txtAmount3.Enabled = true;
                        txtAmount4.Enabled = true;
                        txtAmount5.Enabled = true;
                        txtAmount6.Enabled = true;
                        txtAmount7.Enabled = true;
                        txtAmount8.Enabled = true;
                        txtAmount9.Enabled = true;

                        if (!chBoxMLTST.Checked)
                        {
                            txtID1.Enabled = true;
                            txtID2.Enabled = true;
                            txtID3.Enabled = true;
                            txtID4.Enabled = true;
                            txtID5.Enabled = true;
                            txtID6.Enabled = true;
                            txtID7.Enabled = true;
                            txtID8.Enabled = true;
                            txtID9.Enabled = true;
                            txtID10.Enabled = false;
                        }

                        txt_D1.Enabled = true;
                        txt_D2.Enabled = true;
                        txt_D3.Enabled = true;
                        txt_D4.Enabled = true;
                        txt_D5.Enabled = true;
                        txt_D6.Enabled = true;
                        txt_D7.Enabled = true;
                        txt_D8.Enabled = true;
                        txt_D9.Enabled = true;
                        txt_D0.Enabled = false;

                        break;
                    case 10:
                        txtAmount1.Enabled = true;
                        txtAmount2.Enabled = true;
                        txtAmount3.Enabled = true;
                        txtAmount4.Enabled = true;
                        txtAmount5.Enabled = true;
                        txtAmount6.Enabled = true;
                        txtAmount7.Enabled = true;
                        txtAmount8.Enabled = true;
                        txtAmount9.Enabled = true;
                        txtAmount10.Enabled = true;
                        if (!chBoxMLTST.Checked)
                        {

                            txtID1.Enabled = true;
                            txtID2.Enabled = true;
                            txtID3.Enabled = true;
                            txtID4.Enabled = true;
                            txtID5.Enabled = true;
                            txtID6.Enabled = true;
                            txtID7.Enabled = true;
                            txtID8.Enabled = true;
                            txtID9.Enabled = true;
                            txtID10.Enabled = true;
                        }

                        txt_D1.Enabled = true;
                        txt_D2.Enabled = true;
                        txt_D3.Enabled = true;
                        txt_D4.Enabled = true;
                        txt_D5.Enabled = true;
                        txt_D6.Enabled = true;
                        txt_D7.Enabled = true;
                        txt_D8.Enabled = true;
                        txt_D9.Enabled = true;
                        txt_D0.Enabled = true;
                        break;

                }
            }
            else
            {
                MessageBox.Show("عدد وارد شده نامعتبر است");
                txt_total.Text = "1";
            }
        }                
        private void button1_Click(object sender, EventArgs e)
        {
            txt_resp.Text     = "";
            txt_raw_resp.Text = "";
            
            if (rb_lan.Checked)
            {
                pcpos.ConnectionType = PCPOS.cnType.LAN;
                pcpos.Ip = txtIp.Text;
                pcpos.Port = Int32.Parse(txtPort.Text);
            }
            else
            {
                string str = cmb_bdRate.Text;
                if (str == "") { MessageBox.Show("Please Select BaudRate!!!"); return; }
                pcpos.ConnectionType = PCPOS.cnType.SERIAL;
                pcpos.ComPort = cmb_comport.Text;
            }

            try
            {
                pcpos.send_transaction_Shift_Open();  
                txt_req.Text = pcpos.Request; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            txt_resp.Text = "";
            txt_raw_resp.Text = "";

            if (rb_lan.Checked)
            {
                pcpos.ConnectionType = PCPOS.cnType.LAN;
                pcpos.Ip = txtIp.Text;
                pcpos.Port = Int32.Parse(txtPort.Text);
            }
            else
            {
                string str = cmb_bdRate.Text;
                if (str == "") { MessageBox.Show("Please Select BaudRate!!!"); return; }
                pcpos.ConnectionType = PCPOS.cnType.SERIAL;
                pcpos.ComPort = cmb_comport.Text;
            }

            try
            {
                pcpos.send_transaction_Shift_Close();
                txt_req.Text = pcpos.Request;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            txt_resp.Text = "";
            txt_raw_resp.Text = "";
            pcpos.Amount = txt_amount.Text;
            pcpos.foodSafety = "CompletePack";
            if (txt_signCode.TextLength >= 3)
                pcpos.SignCode = txt_signCode.Text;
            if (chk_params.Checked)
            {
               
                pcpos.Currency = txt_currency.Text;
                pcpos.PrCode   = txt_prcode.Text;
               
                pcpos.R1Holder 	 = txt_r1.Text;
                pcpos.R3Holder	 = txt_r3.Text;
                pcpos.R5Holder	 = txt_r5.Text;
                pcpos.R7Holder 	 = txt_r7.Text;
                pcpos.R9Holder	 = txt_r9.Text;
                pcpos.R2Merchant = txt_r2.Text;
                pcpos.R4Merchant = txt_r4.Text;
                pcpos.R6Merchant = txt_r6.Text;
                pcpos.R8Merchant = txt_r8.Text;
                pcpos.R0Merchant = txt_terminal.Text;
            }

            if (rb_lan.Checked)
            {
                pcpos.ConnectionType = PCPOS.cnType.LAN;
                pcpos.Ip = txtIp.Text;
                pcpos.Port = Int32.Parse(txtPort.Text);
            }
            else
            {
                string str = cmb_bdRate.Text;
                if (str == "") { MessageBox.Show("Please Select BaudRate!!!"); return; }
                pcpos.ConnectionType = PCPOS.cnType.SERIAL;
                pcpos.ComPort = cmb_comport.Text;
            }

            try
            {
                pcpos.send_transaction_food_safety();
                txt_req.Text = pcpos.Request;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        
        private void btn_GLT_Click(object sender, EventArgs e)
        {
            txt_resp.Text = "";
            txt_raw_resp.Text = "";

            if (rb_lan.Checked)
            {
                pcpos.ConnectionType = PCPOS.cnType.LAN;
                pcpos.Ip = txtIp.Text;
                pcpos.Port = Int32.Parse(txtPort.Text);
            }
            else
            {
                string str = cmb_bdRate.Text;
                if (str == "") { MessageBox.Show("Please Select BaudRate!!!"); return; }
                pcpos.ConnectionType = PCPOS.cnType.SERIAL;
                pcpos.ComPort = cmb_comport.Text;
            }

            try
            {
                pcpos.send_transaction_Get_Lats_Trxn();
                txt_req.Text = pcpos.Request;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        
        private void btn_Cancel_Trx_Click(object sender, EventArgs e)
        {
            txt_resp.Text = "";
            txt_raw_resp.Text = "";

            if (rb_lan.Checked)
            {
                pcpos.ConnectionType = PCPOS.cnType.LAN;
                pcpos.Ip = txtIp.Text;
                pcpos.Port = Int32.Parse(txtPort.Text);
            }
            else
            {
                string str = cmb_bdRate.Text;
                if (str == "") { MessageBox.Show("Please Select BaudRate!!!"); return; }
                pcpos.baudRate = Int32.Parse(str);
                pcpos.ConnectionType = PCPOS.cnType.SERIAL;
                pcpos.ComPort = cmb_comport.Text;
            }

            try
            {
                pcpos.send_transaction_Trx_Cancel();
                txt_req.Text = pcpos.Request;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAdvice_Click(object sender, EventArgs e)
        {
            txt_resp.Text = "";
            txt_raw_resp.Text = "";

            if (rb_lan.Checked)
            {
                pcpos.ConnectionType = PCPOS.cnType.LAN;
                pcpos.Ip = txtIp.Text;
                pcpos.Port = Int32.Parse(txtPort.Text);
            }
            else
            {
                string str = cmb_bdRate.Text;
                if (str == "") { MessageBox.Show("Please Select BaudRate!!!"); return; }
                pcpos.baudRate = Int32.Parse(str);
                pcpos.ConnectionType = PCPOS.cnType.SERIAL;
                pcpos.ComPort = cmb_comport.Text;
            }

            try
            {
                pcpos.send_transaction_Trx_Advice();
                txt_req.Text = pcpos.Request;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnReverse_Click(object sender, EventArgs e)
        {
            txt_resp.Text = "";
            txt_raw_resp.Text = "";

            if (rb_lan.Checked)
            {
                pcpos.ConnectionType = PCPOS.cnType.LAN;
                pcpos.Ip = txtIp.Text;
                pcpos.Port = Int32.Parse(txtPort.Text);
            }
            else
            {
                string str = cmb_bdRate.Text;
                if (str == "") { MessageBox.Show("Please Select BaudRate!!!"); return; }
                pcpos.baudRate = Int32.Parse(str);
                pcpos.ConnectionType = PCPOS.cnType.SERIAL;
                pcpos.ComPort = cmb_comport.Text;
            }

            try
            {
                pcpos.send_transaction_Trx_Revers();
                txt_req.Text = pcpos.Request;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnGetTerminalNumber_Click(object sender, EventArgs e)
        {
            if (rb_lan.Checked)
            {
                pcpos.ConnectionType = PCPOS.cnType.LAN;
                pcpos.Ip = txtIp.Text;
                pcpos.Port = Int32.Parse(txtPort.Text);
            }
            else
            {
                string str = cmb_bdRate.Text;
                if (str == "") { MessageBox.Show("Please Select BaudRate!!!"); return; }
                pcpos.baudRate = Int32.Parse(str);
                pcpos.ConnectionType = PCPOS.cnType.SERIAL;
                pcpos.ComPort = cmb_comport.Text;
            }

            try
            {
                pcpos.getTerminalNumber();
                txt_req.Text = pcpos.Request;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void chBoxMLTSLT_CheckedChanged(object sender, EventArgs e)
        {
            if(chBoxMLTST.Checked)
            {
                if (cmBoxMLTSLT.Text.Equals(""))
                {                    
                    MessageBox.Show("select type : MLT/ST ?");
                    chBoxMLTST.Checked = false;
                }
                else
                {
                    chk_params.Checked  = false;
                    chBoxCharge.Checked = false;
                    gb_params.Enabled = chBoxMLTST.Checked;
                    txtID1.Enabled  = false; txtID2.Enabled = false; txtID3.Enabled = false;
                    txtID4.Enabled  = false; txtID5.Enabled = false; txtID6.Enabled = false;
                    txtID7.Enabled  = false; txtID8.Enabled = false; txtID9.Enabled = false;
                    txtID10.Enabled = false;
                }
            }
            else
            {
                chk_params.Checked = false;
                chBoxCharge.Checked = false;
                checkBox1.Checked = false;
            }
        }
        private void chBoxCharge_CheckedChanged(object sender, EventArgs e)
        {
            chBoxMLTST.Checked = false;
            chk_params.Checked = false;
            gb_params.Enabled = false;
            checkBox1.Checked = false;
        }
        private void cmb_bdRate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
