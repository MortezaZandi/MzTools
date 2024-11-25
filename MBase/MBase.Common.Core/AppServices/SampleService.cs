using MBase.Common.Definistions.Interfaces.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBase.Common.Core.AppServices
{
    internal class SampleService : ISampleService
    {
        public string DecorateFileData(string data)
        {
            return $"-- {data} --";
        }
    }
}
