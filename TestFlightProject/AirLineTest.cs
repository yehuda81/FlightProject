using System;
using System.Collections.Generic;
using FlightsManagementSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFlightProject
{
    [TestClass]
    public class AirLineTest
    {
        LoggedInAirlineFacade AirLine = new LoggedInAirlineFacade();        
        LoginToken<AirlineCompany> AirLineLogin = new LoginToken<AirlineCompany>();
        FacadeBase facade = FlyingCenterSystem.GetInstance().GetFacade("LLL", "111", out ILoginToken token);
        Flight flight = new Flight(10079,13,14,new DateTime (2019, 08, 09, 10, 00, 00),new DateTime (2019, 08, 09, 12, 00, 00), 30);
        [TestMethod]
        public void Login()
        {
            Assert.AreEqual(facade.GetType(), AirLine.GetType());
        }

        [TestMethod]
        public void CreateFlight()
        {
            IList<AirlineCompany> airlines = AirLine.GetAllAirlineCompanies();
            flight.AirlineCompanyId = airlines[0].Id;
            AirLine.CreateFlight(AirLineLogin, flight);
        }
        [TestMethod]
        public void GetAllFlights()
        {           
            AirLine.GetAllFlights(AirLineLogin);
        }
        
    }
}
