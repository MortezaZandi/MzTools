using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KyanSetup
{
    public interface IPageNavigator
    {
        void GotoNextPage();
        void GotoPreviousPage();
        bool CanGoNextPage();//if has next page && current page is valid
        bool CanGoPreviousPage();//if has prev page && current page is valid
        
    }
}
