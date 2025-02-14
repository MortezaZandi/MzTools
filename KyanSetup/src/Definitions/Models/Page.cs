using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyanSetup
{
    public class Page
    {
        public Page()
        {
            NextButtonTitle = "Next>";
            BackButtonTitle = "<Back";
        }

        public NavListItemControl PageItemControl { get; set; }
        public BasePageControl PageDetailControl { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string NextButtonTitle { get; set; }
        public string BackButtonTitle { get; set; }
        public bool CanGotoNextPage
        {
            get
            {
                return PageDetailControl.ValidateData();
            }
        }

        public bool CanGotoBackPage
        {
            get
            {
                return !PageDetailControl.InProgress;
            }
        }

        public void CancelOperation()
        {
           PageDetailControl.CancelCurrentOperation();
        }

        internal void OnPageEntered()
        {
            PageDetailControl.OnPageBeignViewed();
        }

        internal void OnPageLeaves()
        {
            PageDetailControl.OnPageBeignLeaved();
        }
    }
}
