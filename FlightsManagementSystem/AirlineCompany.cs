using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class AirlineCompany : IPoco, IUser
    {
        public long Id { get; set; }
        public string AirlineName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long CountryCode { get; set; }

        public AirlineCompany()
        {
        }

        public AirlineCompany(string airlineName, string userName, string password, long countryCode)
        {
            AirlineName = airlineName;
            UserName = userName;
            Password = password;
            CountryCode = countryCode;
        }
        public static bool operator ==(AirlineCompany a, AirlineCompany b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals( b, null))
            {
                return true;
            }
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }
            return a.Id == b.Id;
        }
        public static bool operator !=(AirlineCompany a, AirlineCompany b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"AirlineName: {AirlineName} UserName: {UserName}" +
                $" Password:{Password} CountryCode: {CountryCode}";
        }

        public override bool Equals(object obj)
        {
            AirlineCompany other = obj as AirlineCompany;
            return this == other;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
