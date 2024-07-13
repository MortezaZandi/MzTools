using OLTMockServer.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer
{
    public interface IOrderPatternImporter
    {
        OrderPattern ImportPatternFromFile();
    }
}
