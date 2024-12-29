using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutoFac
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork unitOfWork;
        public PersonService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void SavePerson()
        {
            //save person to database
        }
    }
}
