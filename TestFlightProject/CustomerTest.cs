using System;
using System.Collections.Generic;
using FlightsManagementSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFlightProject
{
    [TestClass]
    public class CustomerTest
    {
        LoggedInCustomerFacade facade = FlyingCenterSystem.GetInstance().GetFacade("YR","1234", out ILoginToken token) as LoggedInCustomerFacade;
        LoginToken<Customer> CustomerLogin = new LoginToken<Customer>();
        
        [TestMethod]
        public void Login()
        {
            Assert.AreEqual(facade.GetType(), facade.GetType());
        }
        [TestMethod]
        public void CancelTicket()
        {
            Ticket ticket = new Ticket(facade.GetAllFlights()[0].Id, 10113);
            facade.CancelTicket(CustomerLogin, ticket);
        }
        [TestMethod]
        public void GetAllMyFlights()
        {            
            facade.GetAllMyFlights(CustomerLogin);
        }
        [TestMethod]
        public void PurchaseTicket()
        {
            AdministratorTest AT = new AdministratorTest();
            LoginToken<Administrator> AdminLogin = new LoginToken<Administrator>();
            Ticket ticket = new Ticket(facade.GetAllFlights()[0].Id, AT.GetCustomerByUserName("YR"));
            facade.PurchaseTicket(CustomerLogin,ticket);
            
        }

    }
}
