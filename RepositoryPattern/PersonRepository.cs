using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context)
        {
        }

        public int GetLastPersonId
        {
            get
            {
                if (AppDbContext.Persons.Any())
                {
                    return AppDbContext.Persons.Max(x => x.Id);
                }
                else
                {
                    return 0;
                }
            }
        }

        public IEnumerable<Person> GetTopOldestPersons(int count)
        {
            return AppDbContext.Persons.OrderByDescending(x => x.Age).Take(count).ToList();
        }

        public IEnumerable<Person> GetTopYoungestPersons(int count)
        {
            return AppDbContext.Persons.OrderBy(x => x.Age).Take(count).ToList();
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
