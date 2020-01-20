using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade
    {
        //AirlineDAOMSSQL Airline = new AirlineDAOMSSQL();
        //CustomerDAOMSSQL Customer = new CustomerDAOMSSQL(); 
        public void CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (token.User != null)
            {
                _airlineDAO.Add(airline);
            }
        }

        public void CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if (token.User != null)
            {
                _customerDAO.Add(customer);
            }
        }
        public Customer GetCustomerByUserName(LoginToken<Administrator> token, string user)
        {
            if (token.User != null)
            {
                return _customerDAO.GetCustomerByUserame(user);
            }
            return null;
        }
        public void CreateNewCountry(LoginToken<Administrator> token, Country country)
        {
            if (token.User != null)
            {
                _countryDAO.Add(country);
            }
        }

        public void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (token.User != null)
            {
                _airlineDAO.Remove(airline);
            }
        }

        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if (token.User != null)
            {
                _customerDAO.Remove(customer);
            }
        }

        public void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (token.User != null)
            {
                _airlineDAO.Update(airline);
            }
        }

        public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            if (token.User != null)
            {
                _customerDAO.Update(customer);
            }
        }
        public AirlineCompany GetAirlineCompanyById(LoginToken<Administrator> token, int id)
        {
            if (token.User != null)
            {
                return _airlineDAO.Get(id);
            }
            return null;
        }

        public Customer GetCustomerById(LoginToken<Administrator> token, int id)
        {
            if (token.User != null)
            {
                return _customerDAO.Get(id);
            }
            return null;
        }
    }
}
