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
            if (token.User.Id == flight.AirlineCompanyId)
            {
                _flightDAO.Remove(flight);
            }            
        }

        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            if (token.User != null)
            {               
                AirlineCompany company = _airlineDAO.GetAirlineByUsername(token.User.UserName);
                if (company.Password.ToString() == oldPassword)
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
            if (token.User.Id == flight.AirlineCompanyId)
            {
                _flightDAO.Add(flight);
            }            
        }

        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            if (token.User != null)
            {
                return _flightDAO.GetAll();
            }
            return null;
        }

        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            if (token.User != null)
            {
                return _ticketDAO.GetAll();
            }
            return null;
        }

        public void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            if (token.User.Id == airline.Id)
            {
                _airlineDAO.Update(airline);
            }            
        }

        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token.User.Id == flight.AirlineCompanyId)
            {
                _flightDAO.Update(flight);
            }            
        }
        
    }
}
