using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            if (token != null)
            {
                _ticketDAO.Remove(ticket);
            }
        }

        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            if (token != null)
            {
                return _flightDAO.GetAll();
            }
            return null;
        }

        public Ticket PurchaseTicket(LoginToken<Customer> token, Ticket ticket)
        {
            if (token != null)
            {
                _ticketDAO.Add(ticket);
                Flight flight = _flightDAO.GetFlightById((int)ticket.FlightId);
                flight.RemainingTickets = flight.RemainingTickets - 1;
                _flightDAO.Update(flight);
                return ticket;               
            }
            return null;
        }
    }
}
