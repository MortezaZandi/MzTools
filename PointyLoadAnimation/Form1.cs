using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointyLoadAnimation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            sPanel1.moveOtherWithMouse(this);
            foreach (Control control in sPanel1.Controls)
            {
                control.moveOtherWithMouse(this);
            }
        }
    }
}
