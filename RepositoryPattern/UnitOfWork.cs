using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly AppDbContext appDbContext;

        public UnitOfWork()
        {
            this.appDbContext = new AppDbContext();
            this.PersonRepository = new PersonRepository(appDbContext);
            this.AddressRepository= new AddressRepository(appDbContext);
        }

        public IPersonRepository PersonRepository { get; private set; }
        public IAddressRepository AddressRepository { get; private set; }
        
        public int Complete()
        {
            return this.appDbContext.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await this.appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.appDbContext?.Dispose();
        }
    }
}
