using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DB_Generator
{   

    class DataDB
    {        
        private const string URL = "https://restcountries.eu/rest/v2";
        private const string URL1 = "https://randomuser.me/api";
        //private const string URL = "http://webapichat110919.azurewebsites.net/api/messages";
        FlightsManagementSystem.CountryDAOMSSQL countryDAOMSSQL = new FlightsManagementSystem.CountryDAOMSSQL();
        FlightsManagementSystem.FlightDAOMSSQL flightDAOMSSQL = new FlightsManagementSystem.FlightDAOMSSQL();
        FlightsManagementSystem.AirlineDAOMSSQL airlineDAOMSSQL = new FlightsManagementSystem.AirlineDAOMSSQL();
        FlightsManagementSystem.CustomerDAOMSSQL customerDAOMSSQL = new FlightsManagementSystem.CustomerDAOMSSQL();
        FlightsManagementSystem.TicketDAOMSSQL ticketDAOMSSQL = new FlightsManagementSystem.TicketDAOMSSQL();
        private static List<string> companiesList = new List<string>();
        private static List<string> countriesList = new List<string>();
        int counter;
        int countriesCounter = 0;
        int companiesCounter = 0;
        int customersCounter = 0;
        int ticketsCounter = 0;
        int flightsCounter = 0;
        int index = 0;
        int index2 = 0;
        Random r = new Random();
        static async Task<Uri> CreateAsync(FlightsManagementSystem.Country country, HttpClient client)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/countries", country);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        // GET REQUEST
        public void AddCountriesToDB(int countries)
        {
            int range = countries;
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<Country>>().Result;
                
                foreach (var d in dataObjects)
                {
                    if (countriesCounter < range)
                    {
                        FlightsManagementSystem.Country country = new FlightsManagementSystem.Country();
                        countriesList.Add(d.name);
                        country.CountryName = d.name;
                        countryDAOMSSQL.Add(country);
                        countriesCounter++;                       
                        counter++;
                        Thread.Sleep(20);                        
                    }
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }

        internal void DeleteDB()
        {
            customerDAOMSSQL.RemoveAll();            
            flightDAOMSSQL.RemoveAll();            
            airlineDAOMSSQL.RemoveAll();            
            countryDAOMSSQL.RemoveAll();                      
        }

        internal int GetCounter()
        {           
            return counter;
        }

        internal void AddCompaniesToDB(int companies)
        {            
            int range = companies;           
            
            for (int i = 0; i < range; i++)
            {
                index = r.Next(countriesList.Count);
                companiesList.Add(AirLinesCompanies.companies[i].UserName);
                FlightsManagementSystem.AirlineCompany company = new FlightsManagementSystem.AirlineCompany();
                company.AirlineName = AirLinesCompanies.companies[i].AirlineName;
                company.UserName = AirLinesCompanies.companies[i].UserName;
                company.Password = AirLinesCompanies.companies[i].Password;
                //company.CountryCode = AirLinesCompanies.companies[i].CountryCode;
                company.CountryCode = countryDAOMSSQL.GetByName(countriesList[index]).Id;
                airlineDAOMSSQL.Add(company);
                counter++;
                companiesCounter++;                
                Thread.Sleep(20);
            }
        }

        internal string getMessegeCountries(int countries)
        {
            if (countriesCounter < countries)
            {
                return $"- Creating Countries ({countriesCounter}/{countries})";

            }
            else
            {
                return $"- {countries} Countries created ";
            }
        }
        internal string getMessegeCompanies(int companies)
        {
            if (companiesCounter < companies)
            {
                return $"- Creating Companies ({companiesCounter}/{companies})";

            }
            else
            {
                return $"- {companies} Companies created ";
            }

        }
        internal string getMessegeCustomers(int customers)
        {
            if (customersCounter < customers)
            {
                return $"- Creating Customers ({customersCounter}/{customers})";

            }
            else
            {
                return $"- {customers} Customers created ";
            }

        }
        internal string getMessegeFlights(int flights)
        {
            if (flightsCounter < flights)
            {
                return $"- Creating Flights ({flightsCounter}/{flights})";

            }
            else
            {
                return $"- {flights} Flights created ";
            }

        }
        internal string getMessegeTickets(int tickets)
        {
            if (ticketsCounter < tickets)
            {
                return $"- Creating Tickets ({ticketsCounter}/{tickets})";

            }
            
            else
            {
                return $"- {tickets} Tickets created ";
            }

        }

        internal void AddTicketsToDB(int tickets)
        {           
            int range = tickets;            
            IList<FlightsManagementSystem.Customer> customers = customerDAOMSSQL.GetAll();
            IList<FlightsManagementSystem.Flight> flights = flightDAOMSSQL.GetAll();
            FlightsManagementSystem.Ticket ticket = new FlightsManagementSystem.Ticket();

            for (int i = 0; i < range; i++)
            {                                  
                    index = r.Next(flights.Count);
                    index2 = r.Next(customers.Count);
                    ticket.FlightId = flights[index].Id;
                    ticket.CustomerId = customers[index2].Id;
                    ticketDAOMSSQL.Add(ticket);
                    flights[index].RemainingTickets--;
                    flightDAOMSSQL.Update(flights[index]);
                    customers.RemoveAt(index2);
                    counter++;
                    ticketsCounter++;
                    Thread.Sleep(20);
                }                
           
        }

        internal void AddFlightsToDB(int flights)
        {            
            int range = flights;
            foreach (var company in companiesList)
            {                
                for (int i = 0; i < range; i++)
                {                       
                    int countryA = r.Next(countriesList.Count);
                    int countryB;
                    do
                    {
                        countryB = r.Next(countriesList.Count);
                    } while (countryB == countryA);                    
                    index = r.Next(companiesList.Count);
                    FlightsManagementSystem.Flight flight = new FlightsManagementSystem.Flight();
                    flight.AirlineCompanyId = airlineDAOMSSQL.GetAirlineByUsername(companiesList[index]).Id;
                    flight.OriginCountryCode = countryDAOMSSQL.GetByName(countriesList[countryA]).Id;
                    flight.DestinationCountryCode = countryDAOMSSQL.GetByName(countriesList[countryB]).Id;
                    DateTime start = new DateTime(DateTime.Today.Year, DateTime.Today.Month,DateTime.Today.Day);                    
                    flight.DepartureTime = start.AddHours(r.Next(120)).AddMinutes(r.Next(60));                    
                    flight.LandingTime = flight.DepartureTime.AddHours(r.Next(24)).AddMinutes(r.Next(60));                    
                    flight.RemainingTickets = 30;
                    flightDAOMSSQL.Add(flight);
                    counter++;
                    flightsCounter++;
                    Thread.Sleep(2);
                }
            }

        }

        internal void AddCustomersToDB(int customers)
        {
            int range = customers;
            FlightsManagementSystem.Customer customer = new FlightsManagementSystem.Customer();
            for (int i = 0; i < range; i++)
            {
                index = r.Next(Customers.firstNames.Count);
                customer.FirstName = Customers.firstNames[index];
                customer.LastName = Customers.lastNames[index];                
                customer.UserName = customer.FirstName + customer.LastName;
                customer.Password = Customers.passwords[index];
                customer.Address = Customers.adresses[index];
                customer.PhoneNo = Customers.phoneNumbers[index];
                customer.CreditCardNumber = r.Next(100000000, 999999999).ToString();
                customerDAOMSSQL.Add(customer);
                Customers.firstNames.RemoveAt(index);
                Customers.lastNames.RemoveAt(index);
                Customers.passwords.RemoveAt(index);
                Customers.adresses.RemoveAt(index);
                Customers.phoneNumbers.RemoveAt(index);
                counter++;
                customersCounter++;
            }
            
        }
    }
}

