using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DbContext context) : base(context)
        {
        }

        public int GetLastAddressId
        {
            get
            {
                if (AppDbContext.Addresses.Any())
                {
                    return AppDbContext.Addresses.Max(x => x.Id);
                }
                else
                {
                    return 0;
                }
            }
        }

        private AppDbContext AppDbContext
        {
            get
            {
                return Context as AppDbContext;
            }
        }
    }
}
