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
            if (token.User != null)
            {
                _ticketDAO.Remove(ticket);
            }
        }

        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            if (token.User != null)
            {
                Customer customer = new Customer();
                customer.Id = token.User.Id;
                customer.UserName = token.User.UserName;
                customer.Password = token.User.Password;
                return _flightDAO.GetFlightsByCustomer(customer);
            }
            return null;
        }

        public Ticket PurchaseTicket(LoginToken<Customer> token, Ticket ticket)
        {
            if (token.User != null)
            {
                _ticketDAO.Add(ticket);
                Flight flight = _flightDAO.GetFlightById((int)ticket.FlightId);
                flight.RemainingTickets = flight.RemainingTickets - 1;
                _flightDAO.Update(flight);
                return ticket;               
            }
            return null;
        }
        public Ticket GetMyLastTicket(LoginToken<Customer> token)
        {
            if (token.User != null)
            {
                return _ticketDAO.GetAll().Last();                
            }
            return null;
        }
        public Ticket GetMyTicket(LoginToken<Customer> token,int Id)
        {
            if (token.User != null)
            {
                return _ticketDAO.Get(Id);
            }
            return null;
        }
    }
}
