using RepositoryPattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public interface IPersonRepository : IRepository<Person>
    {
        IEnumerable<Person> GetTopOldestPersons(int count);
        IEnumerable<Person> GetTopYoungestPersons(int count);
        int GetLastPersonId { get; }

    }
}
