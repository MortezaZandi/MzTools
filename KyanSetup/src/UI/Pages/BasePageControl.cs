using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KyanSetup
{
    public partial class BasePageControl : UserControl
    {
        public BasePageControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// returns true if page is doing an important operation like installation and user should not go to other pages.
        /// </summary>
        public bool InProgress { get; protected set; }

        /// <summary>
        /// returns true if all data in the page are valid and user can go to next page.
        /// </summary>
        /// <returns></returns>
        public virtual bool ValidateData()
        {
            return true;
        }

        public virtual void CancelCurrentOperation()
        {
            //stop all inprogress operations
        }

        /// <summary>
        /// called when user just navigated to this page.
        /// </summary>
        internal virtual void OnPageBeignViewed()
        {

        }

        /// <summary>
        /// called when user leaved this page and navigated to other pages
        /// </summary>
        internal virtual void OnPageBeignLeaved()
        {

        }
    }
}
