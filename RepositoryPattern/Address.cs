using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Alley { get; set; }

        // Foreign key for Person
        public int PersonId { get; set; }

        // Navigation property back to Person
        public virtual Person Person { get; set; }

        public override string ToString()
        {
            return $"{Id}-{City} - {Street} - {Alley}";
        }
    }
}
