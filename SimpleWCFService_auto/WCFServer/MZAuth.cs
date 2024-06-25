using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    public class MZAuthAttribute : Attribute
    {
        public MZAuthAttribute(bool allow)
        {
            Allow = allow;

            if (!allow)
            {
                throw new AuthenticationException("Method not allowed");
            }
        }

        public bool Allow { get; set; }
    }
}
