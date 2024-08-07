﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures
{
    [Serializable]
    public class Customer
    {
        public Customer()
        {
            IsActive = true;
            Code = Utils.GenerateCode(5);
        }

        public long Id { get;  set; }
        public string Code { get; set; }
        public string Name { get;  set; }
        public string Address { get;  set; }
        public string DeliveryType { get;  set; }
        public bool IsActive { get;  set; }
    }
}
