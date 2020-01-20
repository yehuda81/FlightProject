using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class Country : IPoco
    {
        public long Id { get; set; }
        public string CountryName { get; set; }

        public Country()
        {
        }

        public Country(string countryName)
        {
            CountryName = countryName;
        }
        public static bool operator ==(Country a, Country b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }
            return a.Id == b.Id;
        }
        public static bool operator !=(Country a, Country b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            Country other = obj as Country;
            return this == other;
                   
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
