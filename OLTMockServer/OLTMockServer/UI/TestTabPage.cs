using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;

namespace OLTMockServer.UI
{
    public class TestTabPage : RadPageViewPage
    {
        private readonly TestManager testManager;

        public TestTabPage(TestManager testManager)
        {
            this.testManager = testManager;
        }

        public TestManager TestManager => testManager;

        public TestContainerControl TestContainer { get; internal set; }
    }
}
