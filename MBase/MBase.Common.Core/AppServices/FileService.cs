using MBase.Common.Definistions.Interfaces.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBase.Common.Core.AppServices
{
    internal class FileService : IFileService
    {
        public string ReadFirstLine(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                return System.IO.File.ReadAllLines(fileName)[0];
            }

            return null;
        }
    }
}
