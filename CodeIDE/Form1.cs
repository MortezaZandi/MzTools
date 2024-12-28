using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeIDE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.lineNumberedRichTextBox1.KeyDown += LineNumberedRichTextBox1_KeyDown;

            lineNumberedRichTextBox1.Text = @"
//Sample program
//start
efon 1,2,3
goto 100,200,300
goto 11.10,500,-90
//end
efof

";
        }

        private bool dKeyPressed;
        private void LineNumberedRichTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                if (e.KeyCode == Keys.K)
                {
                    dKeyPressed = true;
                }
                else if (e.KeyCode == Keys.D)
                {
                    if (dKeyPressed)
                    {
                        this.lineNumberedRichTextBox1.Format();
                        dKeyPressed = false;
                    }
                }
            }
        }

        private void btnFormatCode_Click(object sender, EventArgs e)
        {
            this.lineNumberedRichTextBox1.Format();
        }
    }
}
