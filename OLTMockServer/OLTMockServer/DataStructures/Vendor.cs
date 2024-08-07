﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures
{
    [Serializable]
    public class Vendor
    {
        public Vendor()
        {
            UID = Guid.NewGuid();
        }

        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public Guid UID { get; set; }

    }
}
