using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PathShortener
{
    public partial class Form1 : Form
    {

        internal const string AppName = "Path Shortner";

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
           (
               int nLeftRect,     // x-coordinate of upper-left corner
               int nTopRect,      // y-coordinate of upper-left corner
               int nRightRect,    // x-coordinate of lower-right corner
               int nBottomRect,   // y-coordinate of lower-right corner
               int nWidthEllipse, // width of ellipse
               int nHeightEllipse // height of ellipse
           );

        public Form1()
        {
            InitializeComponent();

            CreateRoundForm(10);
        }

        private void CreateRoundForm(int radius)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, radius, radius));

            lblTitle.moveOtherWithMouse(this);
            lblTitle.Text = $"{AppName} - {Application.ProductVersion}";


            txtOriginalPath.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, txtOriginalPath.Width, txtOriginalPath.Height, radius / 2, radius / 2));
            txtShortPath.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, txtOriginalPath.Width, txtOriginalPath.Height, radius / 2, radius / 2));
            pnlOrginalPath.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pnlOrginalPath.Width, pnlOrginalPath.Height, radius / 2, radius / 2));
            pnlShortPath.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pnlShortPath.Width, pnlShortPath.Height, radius / 2, radius / 2));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ShowDialogInCurrentScreen();
        }


        private void ShowDialogInCurrentScreen()
        {
            var screen = Screen.FromPoint(Cursor.Position);
            this.StartPosition = FormStartPosition.Manual;
            this.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - this.Width / 2;
            this.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - this.Height / 2;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void ShowError(string message, string title = null)
        {
            MessageBox.Show(
                message,
                title ?? AppName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }


        private void ShowInfo(string message, string title = null)
        {
            MessageBox.Show(
                message,
                title ?? AppName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtOriginalPath.Text))
                {
                    txtShortPath.Text = Win32.GetShortPath(txtOriginalPath.Text);

                    if (txtShortPath.Text.Length == 0)
                    {
                        lblChangeStatus.Text = "Path is not valid";
                        lblChangeStatus.ForeColor = Color.Red;
                    }
                    else if (txtShortPath.Text != txtOriginalPath.Text)
                    {
                        lblChangeStatus.Text = "New Path Created";
                        lblChangeStatus.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblChangeStatus.Text = "Short path is equal to original path";
                        lblChangeStatus.ForeColor = Color.DarkGray;
                    }
                }
                else
                {
                    ShowError("Path is empty");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void txtOriginalPath_TextChanged(object sender, EventArgs e)
        {
            txtShortPath.Clear();
            lblChangeStatus.Text = string.Empty;

            btnOk.Enabled = (txtOriginalPath.Text.Length > 0);

            btnClear.Enabled = (txtOriginalPath.Text.Length > 0) || (txtShortPath.Text.Length > 0);
        }

        private void txtShortPath_TextChanged(object sender, EventArgs e)
        {
            btnCopyShortPath.Visible = (txtShortPath.Text.Length > 0);
        }

        private void btnCopyShortPath_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtShortPath.Text))
                {
                    Clipboard.SetText(txtShortPath.Text);
                }
                else
                {
                    throw new Exception("Short path is empty");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOriginalPath.Clear();
            txtShortPath.Clear();
            lblChangeStatus.Text = string.Empty;
        }
    }
}
