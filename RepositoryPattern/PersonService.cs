using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public void AddNewPerson(Person person)
        {
            throw new NotImplementedException();
        }

        public Person GetLastAddedPerson()
        {
            throw new NotImplementedException();
        }
    }
}
