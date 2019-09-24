using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightsManagementSystem;
using System.Collections.Generic;

namespace TestFlightProject
{
        [TestClass]
    public class AdministratorTest
    {
        TestCenter Test = new TestCenter();
        LoggedInAdministratorFacade Admin = new LoggedInAdministratorFacade();

        LoginToken<Administrator> AdminLogin = new LoginToken<Administrator>();
        FacadeBase facade = FlyingCenterSystem.GetInstance().GetFacade(AppConfig.ADMIN_USER, AppConfig.ADMIN_PASSWORD,out ILoginToken token);
        Customer c = new Customer("Yehuda", "Reisner", "Yuda", "1234", "Rosh Ha-ain", "0545207871", "4580");
        Country country = new Country("Israel");
        AirlineCompany airline = new AirlineCompany("ELAL", "eee", "111", 13);
        [TestMethod]
        public void Login()
        {            
            Assert.AreEqual(facade.GetType(), Admin.GetType());
        }
        [TestMethod]
        public void DeleteData()
        {
            Test.DeleteAllData();
        }
        [TestMethod]
        public void CreateNewCountry()
        {            
            Admin.CreateNewCountry(AdminLogin, country);
        }        
       
        [TestMethod]
        public void CreateNewAirline()
        {
            Admin.CreateNewAirline(AdminLogin, airline);
        }

        [TestMethod]
        public void CreateNewCustomer()
        {            
            Admin.CreateNewCustomer(AdminLogin, c);
        }

        [TestMethod]
        public void RemoveAirline()
        {
            IList<AirlineCompany> airlines = Admin.GetAllAirlineCompanies();            
            Admin.RemoveAirline(AdminLogin, airlines[0]);
        }
        [TestMethod]
        public void RemoveCustomer()
        {
            Admin.RemoveCustomer(AdminLogin,c);            
        }
        [TestMethod]
        public void UpdateAirlineDetails()
        {
            CreateNewAirline();
            IList<AirlineCompany> airlines = Admin.GetAllAirlineCompanies();
            airlines[0].UserName = "LLL";            
            Admin.UpdateAirlineDetails(AdminLogin, airlines[0]);
        }
        [TestMethod]
        public void UpdateCustomerDetails()
        {
            Customer customer = Admin.GetCustomerByUserName(AdminLogin, c.UserName);
            customer.UserName = "YR";
            Admin.UpdateCustomerDetails(AdminLogin, customer);
        }
    }
}
