using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph2D
{
    public partial class Form1 : Form
    {
        private Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            graphControl1.Invalidate();
        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            var nd = DateTime.Now.Second;
            //nd = rnd.Next(0, 1000);
            graphControl1.AddData(nd);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = $"2D Graph - Time : {DateTime.Now:HH:mm:ss}";
        }
    }
}
