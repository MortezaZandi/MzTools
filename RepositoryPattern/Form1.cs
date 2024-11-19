using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepositoryPattern
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var lastAddedPerson = unitOfWork.PersonRepository.GetLastPersonId;
                
                var person = new Person
                {
                    Id = lastAddedPerson + 1,
                    Name = "M1",
                    FamilyName = "M1.F1",
                    Age = 33,
                    Status = "AC",
                    IsDeleted = false,
                };

                unitOfWork.PersonRepository.Add(person);

                var address = new Address
                {
                    Id = unitOfWork.AddressRepository.GetLastAddressId + 1,
                    City = "Tehran",
                    Street = "Ghandi",
                    Alley = "18",
                    PersonId = person.Id,
                };

                unitOfWork.AddressRepository.Add(address);

                await unitOfWork.CompleteAsync();

                lastAddedPerson = unitOfWork.PersonRepository.GetLastPersonId;

                MessageBox.Show($"Person {lastAddedPerson} added successfully.", "Add perosn");
            }
        }

        private void btnReadLast_Click(object sender, EventArgs e)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var lastAddedPerson = unitOfWork.PersonRepository.Get(unitOfWork.PersonRepository.GetLastPersonId);

                MessageBox.Show($"Id: {lastAddedPerson.Id}\nName: {lastAddedPerson.Name} {lastAddedPerson.FamilyName}\nAddress: {lastAddedPerson.Addresses.FirstOrDefault()?.ToString()}", "Last added person");
            }
        }

    }
}
