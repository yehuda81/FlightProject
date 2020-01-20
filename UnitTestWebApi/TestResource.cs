using FlightsManagementSystem;
using System;

namespace UnitTestWebApi
{
    internal class TestResource
    {
        public static Flight flight = new Flight()
        {
            AirlineCompanyId = 31192,
            DepartureTime = new DateTime(2020, 01, 19, 21, 01, 000),
            LandingTime = new DateTime(2020, 01, 20, 20, 57, 000),
            DestinationCountryCode = 21962,
            OriginCountryCode = 21958
           
        };
        public static Customer newCustomer = new Customer()
        {
            FirstName = "israel",
            LastName = "israeli",
            UserName = "ils",
            Password = "12345",
            Address = "Tel Aviv",
            PhoneNo = "054-5207871",
            CreditCardNumber = "4580 1010 2020 4040"
        };
        
        
    }
}