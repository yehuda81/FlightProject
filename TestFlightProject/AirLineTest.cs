using System;
using System.Collections.Generic;
using FlightsManagementSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFlightProject
{
    [TestClass]
    public class AirLineTest
    {
        LoggedInAirlineFacade facade = FlyingCenterSystem.GetInstance().GetFacade("LLL", "111", out ILoginToken token) as LoggedInAirlineFacade;
        LoginToken<AirlineCompany> AirLineLogin = new LoginToken<AirlineCompany>();
        Flight flight = new Flight(10079,13,14,new DateTime (2019, 08, 09, 10, 00, 00),new DateTime (2019, 08, 09, 12, 00, 00), 30);
        [TestMethod]
        public void Login()
        {
            Assert.AreEqual(facade.GetType(), facade.GetType());
        }

        [TestMethod]
        public void CreateFlight()
        {
            IList<AirlineCompany> airlines = facade.GetAllAirlineCompanies();
            flight.AirlineCompanyId = airlines[0].Id;
            facade.CreateFlight(AirLineLogin, flight);
        }
        [TestMethod]
        public void GetAllFlights()
        {           
            facade.GetAllFlights(AirLineLogin);
        }
        [TestMethod]
        public void UpdateFlight()
        {
            IList<AirlineCompany> airlines = facade.GetAllAirlineCompanies();
            flight.AirlineCompanyId = airlines[0].Id;
            flight.LandingTime = new DateTime(2019, 11, 11, 10, 00, 00);
            facade.UpdateFlight(AirLineLogin, flight);
        }
        [TestMethod]
        public void ModifyAirlineDetails()
        {
            IList<AirlineCompany> airlines = facade.GetAllAirlineCompanies();
            airlines[0].UserName = "LLL";
            facade.ModifyAirlineDetails(AirLineLogin, airlines[0]);
        }
        [TestMethod]
        public void CancelFlight()
        {
            IList<AirlineCompany> airlines = facade.GetAllAirlineCompanies();
            flight.AirlineCompanyId = airlines[0].Id;
            facade.CancelFlight(AirLineLogin, flight);
        }
        [TestMethod]
        public void GetAllTickets()
        {
            IList<Ticket> tickets = facade.GetAllTickets(AirLineLogin);
        }
        [TestMethod]
        public void ChangeMyPassword()
        {
            LoginToken<AirlineCompany> AirLineLogin = new LoginToken<AirlineCompany>
            {
                User = facade.GetAllAirlineCompanies()[0]
            };
            facade.ChangeMyPassword(AirLineLogin, "111", "222");            
        }
    }
}
