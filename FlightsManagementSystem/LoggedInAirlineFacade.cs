using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade
    {        
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token != null)
            {
                _flightDAO.Remove(flight);
            }            
        }

        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            if (token != null)
            {
                AirlineCompany company = _airlineDAO.GetAirlineByUsername(token.ToString());
                if (company.Password == oldPassword)
                {
                    company.Password = newPassword;
                    _airlineDAO.Update(company);
                }
                else
                {
                    throw new WrongPasswordException("Wrong Password");
                }
            }            
            
        }

        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token != null)
            {
                _flightDAO.Add(flight);
            }            
        }

        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            if (token != null)
            {
                return _flightDAO.GetAll();
            }
            return null;
        }

        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            if (token != null)
            {
                return _ticketDAO.GetAll();
            }
            return null;
        }

        public void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            if (token != null)
            {
                _airlineDAO.Update(airline);
            }            
        }

        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token != null)
            {
                _flightDAO.Update(flight);
            }            
        }
    }
}
