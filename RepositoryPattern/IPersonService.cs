using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public interface IPersonService
    {
        void AddNewPerson(Person person);
        Person GetLastAddedPerson();
    }
}
