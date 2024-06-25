using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures.Snap
{
    [Serializable]
    public class SnapItem : Item
    {
        public bool Bundle { get; set; }
    }
}
