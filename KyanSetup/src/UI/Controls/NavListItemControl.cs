using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KyanSetup
{
    public class NavListItemControl : Label
    {
        private bool _selected = false;
        public NavListItemControl()
        {
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;

                    if (_selected)
                    {
                        this.Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Bold);
                    }
                    else
                    {
                        this.Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                    }
                }
            }
        }
    }
}
