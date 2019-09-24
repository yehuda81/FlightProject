using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public interface ILoggedInAdministratorFacade
    {
        void CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline);
        Customer GetCustomerByUserName(LoginToken<Administrator> token, string user);
        void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline);
        void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline);
        void CreateNewCustomer(LoginToken<Administrator> token, Customer customer);
        void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer);
        void RemoveCustomer(LoginToken<Administrator> token, Customer customer);
    }
}
