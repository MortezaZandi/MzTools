using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutoFac
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork unitOfWork;
        public AddressService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Save()
        {
            //save address
        }
    }
}
