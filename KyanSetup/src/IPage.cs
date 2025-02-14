using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KyanSetup
{
    public interface IPage
    {
        IPageNavigator PageNavigator { get; set; }

        bool IsValidForNavigateToNextPage();

        string PageTitle { get; set; }
        string PageDescription { get; set; }
        
    }
}
