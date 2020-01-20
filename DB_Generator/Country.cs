using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Generator
{
    class Country
    {
        public long id;
        public string name;       

        public Country(long id, string name)
        {
            this.id = id;
            this.name = name;            
        }
    }
}
