using RepositoryPattern.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;

namespace RepositoryPattern
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("Data Source=ks40;Initial Catalog=CMSDB;Integrated Security=False;User ID=sa;Password=kyan@@123")
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasKey(p=>p.Id)
                .HasMany(p => p.Addresses);


            modelBuilder.Entity<Address>()
                .HasKey(a => a.Id)
                .HasRequired(a => a.Person);

            base.OnModelCreating(modelBuilder);
        }
    }
}
