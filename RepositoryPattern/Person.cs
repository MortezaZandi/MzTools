using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    [Table("Persons")]
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation property for related addresses
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
