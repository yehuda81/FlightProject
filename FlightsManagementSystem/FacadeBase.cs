using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public abstract class FacadeBase
    {
        protected IAirlineDAO _airlineDAO;
        protected ICountryDAO _countryDAO;
        protected ICustomerDAO _customerDAO;
        protected IFlightDAO _flightDAO;
        protected ITicketDAO _ticketDAO;

        public FacadeBase()
        {
            _airlineDAO = new AirlineDAOMSSQL();
            _countryDAO = new CountryDAOMSSQL();
            _customerDAO = new CustomerDAOMSSQL();
            _flightDAO = new FlightDAOMSSQL();
            _ticketDAO = new TicketDAOMSSQL();
        }
    }
}
