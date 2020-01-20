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
    public class AnonymousFacadeController : ApiController
    {
        AnonymousUserFacade facade = new AnonymousUserFacade();

        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/AnonymousFacade/GetAllFlights")]
        public IHttpActionResult GetAllFlights()
        {
            IList<Flight> flights = facade.GetAllFlights();
            if (flights.Count > 0)
            {
                return Ok(flights);
            }
            return NotFound();

        }

        [ResponseType(typeof(AirlineCompany))]
        [HttpGet]
        [Route("api/AnonymousFacade/GetAllAirlineCompanies")]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            IList<AirlineCompany> airlines = facade.GetAllAirlineCompanies();
            if (airlines.Count > 0)
            {
                return Ok(airlines);
            }
            return NotFound();

        }

        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/AnonymousFacade/GetAllFlightsVacancy")]
        public IHttpActionResult GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flights = facade.GetAllFlightsVacancy();
            if (flights.Count > 0)
            {
                return Ok(flights);
            }
            return NotFound();

        }

        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/AnonymousFacade/GetFlightById/{id}")]
        public IHttpActionResult GetFlightById(int id)
        {
            Flight flight = facade.GetFlightById(id);
            if (flight.Id != 0)
            {
                return Ok(flight);
            }
            return NotFound();
        }

        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/AnonymousFacade/GetFlightsByOriginCountry/{countryCode}")]
        public IHttpActionResult GetFlightsByOriginCountry(int countryCode)
        {
            IList<Flight> flights = facade.GetFlightsByOriginCountry(countryCode);
            if (flights.Count > 0)
            {
                return Ok(flights);
            }
            return NotFound();
        }

        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/AnonymousFacade/GetFlightsByDestinationCountry/{countryCode}")]
        public IHttpActionResult GetFlightsByDestinationCountry(int countryCode)
        {
            IList<Flight> flights = facade.GetFlightsByDestinationCountry(countryCode);
            if (flights.Count > 0)
            {
                return Ok(flights);
            }
            return NotFound();
        }

        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/AnonymousFacade/GetFlightsByDepatrureDate/{departureDate}")]
        public IHttpActionResult GetFlightsByDepatrureDate(DateTime departureDate)
        {
            IList<Flight> flights = facade.GetFlightsByDepatrureDate(departureDate);
            if (flights.Count > 0)
            {
                return Ok(flights);
            }
            return NotFound();
        }

        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/AnonymousFacade/GetFlightsByLandingDate/{landingDate}")]
        public IHttpActionResult GetFlightsByLandingDate(DateTime landingDate)
        {
            IList<Flight> flights = facade.GetFlightsByLandingDate(landingDate);
            if (flights.Count > 0)
            {
                return Ok(flights);
            }
            return NotFound();
        }
        // query string
        // ...api/flights/search ? airlineCompanyId = d & originCountryCode = o
        // ...api/flights/search ? originCountryCode = o & airlineCompanyId = d 
        [Route("api/flights/search")]
        [HttpGet]
        public IEnumerable<Flight> GetByFilter(string airlineCompanyId = "", string originCountryCode = "")
        {
            if (airlineCompanyId == "" && originCountryCode != "")
                return facade.GetAllFlights().Where(m => m.OriginCountryCode.ToString().ToUpper().Contains(originCountryCode.ToUpper()));
            if (airlineCompanyId != "" && originCountryCode == "")
                return facade.GetAllFlights().Where(m => m.AirlineCompanyId.ToString().ToUpper().Contains(airlineCompanyId.ToUpper())); ;
            return facade.GetAllFlights().Where(m => m.AirlineCompanyId.ToString().ToUpper().Contains(airlineCompanyId.ToUpper()) && m.OriginCountryCode.ToString().ToUpper().Contains(originCountryCode.ToUpper()));
        }
    }
}
