using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace OLTMockServer.UI
{
    public partial class DataDialog : RadForm, IConfirmableDialog
    {
        private readonly IDataControl childControl;
        public DataDialog(IDataControl childControl, string title)
        {
            InitializeComponent();
            this.childControl = childControl;
            this.childControl.ParentDialog = this;
            this.Text = title;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var embed = childControl as Control;

            this.Controls.Add((Control)childControl);

            //if (embed.Width > this.Width || embed.Height > this.Height)
            //{
            //    this.ClientSize = new Size(embed.Width, embed.Height);
            //    embed.Location = new Point(0, 0);
            //}
            //else
            //{
            this.childControl.Dock = DockStyle.Fill;
            //}
        }

        public void Cancel()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void OK()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (SingleInstance && !closeRequested)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        private bool closeRequested;
        public bool SingleInstance { get; set; }
    }
}
