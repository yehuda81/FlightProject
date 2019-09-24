using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public interface IAirlineDAO :IBasicDB<AirlineCompany>
    {
        AirlineCompany GetAirlineByUsername(string name);
        IList<AirlineCompany> GetAllAirlinesByCountry(int countryId);
    }
}
