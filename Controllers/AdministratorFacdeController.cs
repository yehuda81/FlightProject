using FlightsManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace FlightsWebApplication.Controllers
{
    [BasicAuthenticationAdministrator]
    public class AdministratorFacdeController : ApiController
    {
        LoggedInAdministratorFacade facade = new LoggedInAdministratorFacade(); 
        LoginToken<Administrator> AdminLogin = new LoginToken<Administrator>();

        [ResponseType(typeof(AirlineCompany))]
        [HttpGet]
        [Route("api/AdministratorFacade/airlines/byname/{text}")]        
        public IEnumerable<AirlineCompany> GetByTextAndSender([FromUri] string text)
        {
            IEnumerable<AirlineCompany> result_text = facade.GetAllAirlineCompanies().Where(m => m.AirlineName.ToUpper().Contains(text.ToUpper()));            
            return result_text;
        }

        [ResponseType(typeof(AirlineCompany))]
        [HttpPost]
        [Route("api/AdministratorFacade/CreateNewAirline")]
        public IHttpActionResult CreateNewAirline([FromBody] AirlineCompany airline)
        {
            GetLoginToken();
            try
            {                
                facade.CreateNewAirline(AdminLogin,airline);
                AirlineCompany company = facade.GetAllAirlineCompanies().Last();
                airline.Id = company.Id;
                if (company == airline)
                {                    
                    return Ok(airline);
                }
                return BadRequest();                
            }

            catch (Exception)
            {
                return BadRequest(); 
            } 
        }
        [ResponseType(typeof(AirlineCompany))]
        [HttpPost]
        [Route("api/AdministratorFacade/UpdateAirlineDetails/{id}")]
        public IHttpActionResult UpdateAirlineDetails([FromUri] int id, [FromBody] AirlineCompany company)
        {
            GetLoginToken();
            try
            {                
                company.Id = id;
                facade.UpdateAirlineDetails(AdminLogin, company);
                if (company == facade.GetAirlineCompanyById(AdminLogin, id))
                {
                    return Ok(company);
                }
                return BadRequest();                
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        
        [ResponseType(typeof(AirlineCompany))]
        [HttpPost]
        [Route("api/AdministratorFacade/RemoveAirline/{id}")]
        public IHttpActionResult RemoveAirline([FromUri] int id)
        {
            GetLoginToken();
            AirlineCompany airline = facade.GetAirlineCompanyById(AdminLogin, id);
            if (airline.Id == id)
            {
                facade.RemoveAirline(AdminLogin, airline);
                return Ok();
            }
            return NotFound();
        }
        
        [ResponseType(typeof(Customer))]
        [HttpPost]
        [Route("api/AdministratorFacade/CreateNewCustomer")]
        public IHttpActionResult CreateNewCustomer([FromBody] Customer customer)
        {
            GetLoginToken();
            try
            {
                facade.CreateNewCustomer(AdminLogin, customer);
                Customer c = facade.GetCustomerByUserName(AdminLogin, customer.UserName);
                c.Id = customer.Id;
                if (c == customer)
                {
                    return Ok(customer);
                }
                return BadRequest();
                
            }

            catch (Exception)
            {
                return NotFound();
            }

        }
        [ResponseType(typeof(Customer))]
        [HttpPost]
        [Route("api/AdministratorFacade/UpdateCustomerDetails/{id}")]
        public IHttpActionResult UpdateCustomerDetails([FromUri] int id, [FromBody] Customer customer)
        {
            GetLoginToken();
            try
            {               
                customer.Id = id;
                facade.UpdateCustomerDetails(AdminLogin, customer);
                if (customer == facade.GetCustomerById(AdminLogin, id))
                {
                    return Ok(customer);
                }
                return BadRequest();
            }

            catch (Exception)
            {
                return NotFound();
            }

        }
        [ResponseType(typeof(Customer))]
        [HttpPost]
        [Route("api/AdministratorFacade/RemoveCustomer/{id}")]
        public IHttpActionResult RemoveCustomer([FromUri] int id)
        {
            GetLoginToken();
            try
            {
                Customer customer = facade.GetCustomerById(AdminLogin, id);
                facade.RemoveCustomer(AdminLogin, customer);
                Customer c = facade.GetCustomerById(AdminLogin, id);
                if (c.Id == id)
                {
                    facade.RemoveCustomer(AdminLogin, customer);
                    return Ok();
                }
                return BadRequest();
            }

            catch (Exception)
            {
                return NotFound();
            }

        }

        private void GetLoginToken()
        {
            Request.Properties.TryGetValue("token", out object Token);
            Administrator administrator = (Administrator)Token;
            AdminLogin.User = administrator;
        }
    }
}
