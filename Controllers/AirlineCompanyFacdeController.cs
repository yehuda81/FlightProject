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
    [BasicAuthenticationAirLine]
    public class AirlineCompanyFacdeController : ApiController
    {
        LoggedInAirlineFacade airlineFacade = new LoggedInAirlineFacade();
        LoginToken<AirlineCompany> loginToken = new LoginToken<AirlineCompany>();

        [ResponseType(typeof(Ticket))]
        [HttpGet]
        [Route("api/airlineFacade/GetAllTickets")]
        public IHttpActionResult GetAllTickets()
        {
            GetLoginToken();

            try
            {
                IList<Ticket> tickets = airlineFacade.GetAllTickets(loginToken);
                if (tickets.Count == 0)
                {
                    return NotFound();
                }
                return Ok(tickets);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/airlineFacade/GetAllFlights")]
        public IHttpActionResult GetAllFlights()
        {
            GetLoginToken();

            try
            {
                IList<Flight> flights = airlineFacade.GetAllFlights(loginToken);
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

        [ResponseType(typeof(Flight))]
        [HttpPost]
        [Route("api/airlineFacade/CancelFlight/{id}")]
        public IHttpActionResult CancelFlight(int id)
        {
            GetLoginToken();

            try
            {
                Flight flight = airlineFacade.GetFlightById(id);
                airlineFacade.CancelFlight(loginToken, flight);
                if (airlineFacade.GetFlightById(id).Id == 0 && flight.Id !=0)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [ResponseType(typeof(Flight))]
        [HttpPost]
        [Route("api/airlineFacade/CreateFlight")]
        public IHttpActionResult CreateFlight([FromBody]Flight flight)
        {
            GetLoginToken();

            try
            {                
                airlineFacade.CreateFlight(loginToken, flight);
                Flight f = airlineFacade.GetAllFlights(loginToken).Last();
                flight.Id = f.Id;
                if (f == flight)
                {
                    return Ok(flight);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [ResponseType(typeof(Flight))]
        [HttpPost]
        [Route("api/airlineFacade/UpdateFlight/{id}")]
        public IHttpActionResult UpdateFlight([FromBody]Flight flight, int id)
        {
            GetLoginToken();

            try
            {
                flight.Id = id;
                airlineFacade.UpdateFlight(loginToken, flight);
                if (airlineFacade.GetFlightById(id).Id == id && id != 0)
                {
                    return Ok(flight);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [ResponseType(typeof(AirlineCompany))]
        [HttpPost]
        [Route("api/airlineFacade/ChangeMyPassword/{oldPassword}/{newPassword}")]
        public IHttpActionResult ChangeMyPassword(string oldPassword, string newPassword)
        {
            GetLoginToken();

            try
            {
                //if (oldPassword == loginToken.User.Password)
                //{
                    airlineFacade.ChangeMyPassword(loginToken, oldPassword, newPassword);
                    return Ok("Password changed successfully");
               // }
                
                //return BadRequest("incorrect password");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [ResponseType(typeof(AirlineCompany))]
        [HttpPost]
        [Route("api/airlineFacade/MofidyAirlineDetails")]
        public IHttpActionResult MofidyAirlineDetails([FromBody]AirlineCompany airline)
        {
            GetLoginToken();

            try
            {                
                if (airline.Id == loginToken.User.Id)
                {
                    airlineFacade.ModifyAirlineDetails(loginToken, airline);
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private void GetLoginToken()
        {
            Request.Properties.TryGetValue("token", out object Token);
            AirlineCompany airline = (AirlineCompany)Token;
            loginToken.User = airline;
        }
    }
}
