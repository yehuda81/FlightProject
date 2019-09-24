using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class LoginService : ILoginService
    {
        private IAirlineDAO _airlineDAO;
        private ICustomerDAO _customerDAO;        
        public LoginService()
        {
            _airlineDAO = new AirlineDAOMSSQL();
            _customerDAO = new CustomerDAOMSSQL();
        }
        public bool TryAdminLogin(string userName, string password, out LoginToken<Administrator> token)
        {
            if (userName == AppConfig.ADMIN_USER && password == AppConfig.ADMIN_PASSWORD)
            {
                token = new LoginToken<Administrator>();
                token.User = new Administrator(userName,password);
                return true;
            }

            token = null;
            return false;
        }

        public bool TryAirlineLogin(string userName, string password, out LoginToken<AirlineCompany> token)
        {
            AirlineCompany company = _airlineDAO.GetAirlineByUsername(userName);
            if (company != null)
            {
                if (company.UserName == userName)
                {
                    if (company.Password == password)
                    {
                        token = new LoginToken<AirlineCompany>();
                        token.User = company;
                        return true;
                    }
                    else
                    {
                        throw new WrongPasswordException("Wrong Password");
                    }
                }
            }
            token = null;
            return false;            
        }

        public bool TryCustomerLogin(string userName, string password, out LoginToken<Customer> token)
        {
            Customer customer = _customerDAO.GetCustomerByUserame(userName);           
            if (customer != null)
            {
                if (customer.UserName == userName)
                {
                    if (customer.Password == password)
                    {
                        token = new LoginToken<Customer>();
                        token.User = customer;
                        return true;
                    }
                    else
                    {
                        throw new WrongPasswordException("Wrong Password");
                    }
                }
            }
            token = null;
            return false;
        }
    }
}
