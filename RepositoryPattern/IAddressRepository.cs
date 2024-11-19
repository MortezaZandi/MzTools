using RepositoryPattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public interface IAddressRepository : IRepository<Address>
    {
        int GetLastAddressId { get; }
    }
}
