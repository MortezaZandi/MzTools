using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutoFac
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
            ID = DateTime.Now.Second;
        }

        public int ID { get; set; }

        public void Save()
        {
            //save data to database
        }

        public override string ToString()
        {
            return $"<{ID}>";
        }
    }
}
