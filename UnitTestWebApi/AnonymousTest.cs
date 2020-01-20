using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using FlightsManagementSystem;
using System.Collections.Generic;

namespace UnitTestWebApi
{
    [TestClass]
    public class AnonymousTest
    {
        [TestMethod]
        public void TestAnonymousController_GetAllFlights()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("");
                string ss = Convert.ToBase64String(plainTextBytes);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = client.GetAsync("http://localhost:50946/api/AnonymousFacade/GetAllFlights").Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        IList<Flight> flights = content.ReadAsAsync<IList<Flight>>().Result;

                        Assert.AreNotEqual(null, flights);
                        Assert.AreEqual(TestResource.flight.AirlineCompanyId, flights[8].AirlineCompanyId);
                        Assert.AreEqual(TestResource.flight.DepartureTime, flights[8].DepartureTime);
                        Assert.AreEqual(TestResource.flight.DestinationCountryCode, flights[8].DestinationCountryCode);
                        Assert.AreEqual(TestResource.flight.OriginCountryCode, flights[8].OriginCountryCode);
                        Assert.AreEqual(TestResource.flight.LandingTime, flights[8].LandingTime);
                    }
                }
              
                
            }
        }

        [TestMethod]
        public void TestAdministratorFacade_CreateNewCustomer()
        {
            using (HttpClient client = new HttpClient())
            {                
                client.DefaultRequestHeaders.Accept.Clear();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("admin:9999");
                string ss = Convert.ToBase64String(plainTextBytes);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(plainTextBytes));

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                
                Customer customer =  TestResource.newCustomer;
               
                using (HttpResponseMessage response = client.PostAsJsonAsync("http://localhost:50946/api/AdministratorFacade/CreateNewCustomer",customer).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        Customer testCustomer = content.ReadAsAsync<Customer>().Result;
                        Assert.AreNotEqual(null, testCustomer);
                        Assert.AreEqual(customer.FirstName, testCustomer.FirstName);
                        Assert.AreEqual(customer.LastName, testCustomer.LastName);
                        Assert.AreEqual(customer.UserName, testCustomer.UserName);
                        Assert.AreEqual(customer.Password, testCustomer.Password);
                        Assert.AreEqual(customer.Address, testCustomer.Address);
                        Assert.AreEqual(customer.PhoneNo, testCustomer.PhoneNo);
                        Assert.AreEqual(customer.CreditCardNumber, testCustomer.CreditCardNumber);
                        
                    }
                }


            }
        }
        [TestMethod]
        public void TestAirlineFacade_ChangeMyPassword()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("TUS:323");
                string ss = Convert.ToBase64String(plainTextBytes);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(plainTextBytes));
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                AirlineCompany airline = new AirlineCompany();
                airline.UserName = "TUS";
                airline.Password = "323";
                using (HttpResponseMessage response = client.PostAsJsonAsync("http://localhost:50946/api/airlineFacade/ChangeMyPassword/323/323",airline).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        Assert.AreEqual("OK", response.ReasonPhrase.ToString());                        
                    }
                }


            }
        }
        [TestMethod]
        public void TestCustomerFacade_CancelTicket()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("Anthony  Mansir :1e!Ck");
                string ss = Convert.ToBase64String(plainTextBytes);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(plainTextBytes));
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                Customer customer = new Customer();
                customer.UserName = "Anthony  Mansir";
                customer.Password = "1e!Ck";
                using (HttpResponseMessage response = client.PostAsJsonAsync("http://localhost:50946/api/CustomerFacade/CancelTicket/20846", customer).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        Assert.AreEqual("OK", response.ReasonPhrase.ToString());
                    }
                }


            }
        }

    }
}

