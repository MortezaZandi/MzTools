using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBase.Common.Definistions.Interfaces.AppServices
{
    public interface IFileService
    {
        string ReadFirstLine(string fileName);
    }
}
