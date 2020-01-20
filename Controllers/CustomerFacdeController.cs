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
    [BasicAuthenticationCustomer]
    public class CustomerFacdeController : ApiController
    {        
        LoggedInCustomerFacade customerFacade = new LoggedInCustomerFacade();
        LoginToken<Customer> loginToken = new LoginToken<Customer>();  
        
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/CustomerFacade/GetAllMyFlights")]
        public IHttpActionResult GetAllMyFlights()
        {
            GetLoginToken();
                    
            try
            {
                IList<Flight> flights = customerFacade.GetAllMyFlights(loginToken);
                if (flights.Count == 0)
                {
                    return NotFound();
                }
                return Ok(flights);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private void GetLoginToken()
        {
            Request.Properties.TryGetValue("token", out object Token);
            Customer customer = (Customer)Token;
            loginToken.User = customer;            
        }

        [ResponseType(typeof(Ticket))]
        [HttpPost]
        [Route("api/CustomerFacade/PurchaseTicket/{id}")]
        public IHttpActionResult PurchaseTicket([FromUri] long Id)
        {
            GetLoginToken();
            try
            {
                Ticket ticket = new Ticket(Id, loginToken.User.Id);
                customerFacade.PurchaseTicket(loginToken, ticket);
                return Ok(customerFacade.GetMyLastTicket(loginToken));
            }
            catch (Exception)
            {
                return BadRequest();
            }
            

        }
        [ResponseType(typeof(Ticket))]
        [HttpPost]
        [Route("api/CustomerFacade/CancelTicket/{id}")]
        public IHttpActionResult CancelTicket([FromUri] int Id)
        {
            GetLoginToken();
            try
            {
                Ticket ticket = customerFacade.GetMyTicket(loginToken, Id);
                if (ticket.Id != 0)
                {
                    customerFacade.CancelTicket(loginToken, ticket);
                    return Ok($"Ticket {Id} canceld");
                }
                return NotFound();                
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
