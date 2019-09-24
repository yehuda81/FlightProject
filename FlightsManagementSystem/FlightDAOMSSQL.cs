using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class FlightDAOMSSQL : IFlightDAO
    {
        public void Add(Flight t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                SqlCommand cmd = new SqlCommand("ADD_FLIGHT", con);
                cmd.Parameters.Add(new SqlParameter("@AIRLINECOMPANY_ID", t.AirlineCompanyId));
                cmd.Parameters.Add(new SqlParameter("@ORIGIN_COUNTRY_CODE", t.OriginCountryCode));
                cmd.Parameters.Add(new SqlParameter("@DESTINATION_COUNTRY_CODE", t.DestinationCountryCode));
                cmd.Parameters.Add(new SqlParameter("@DEPARTURE_TIME", t.DepartureTime));
                cmd.Parameters.Add(new SqlParameter("@LANDING_TIME", t.LandingTime));
                cmd.Parameters.Add(new SqlParameter("@REMAINING_TICKETS", t.RemainingTickets));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        internal void MoveFlightsToFlightstHistory()
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                SqlCommand cmd = new SqlCommand("MOVE_FLIGHTS_TO_HISTORY", con);
                cmd.Parameters.Add(new SqlParameter("@Trheehoursago", DateTime.Now.AddHours(-3)));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Flight Get(int id)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            Flight t = new Flight();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_FLIGHT_BY_ID", con);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        t.Id = (long)reader["ID"];
                        t.AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"];
                        t.OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"];
                        t.DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"];
                        t.DepartureTime = (DateTime)reader["DEPARTURE_TIME"];
                        t.LandingTime = (DateTime)reader["LANDING_TIME"];
                        t.RemainingTickets = (int)reader["REMAINING_TICKETS"];
                    }
                    con.Close();
                    return t;
                }
            }
        }

        public IList<Flight> GetAll()
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            List<Flight> flights = new List<Flight>();
            Flight t = new Flight();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_ALL_FLIGHT", con);                
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        t.Id = (long)reader["ID"];
                        t.AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"];
                        t.OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"];
                        t.DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"];
                        t.DepartureTime = (DateTime)reader["DEPARTURE_TIME"];
                        t.LandingTime = (DateTime)reader["LANDING_TIME"];
                        t.RemainingTickets = (int)reader["REMAINING_TICKETS"];
                        flights.Add(t);
                    }
                    con.Close();
                    return flights;
                }
            }
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            Dictionary<Flight,int> vacancyFlights = new Dictionary<Flight,int>();
            Flight t = new Flight();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_ALL_FLIGHTS_VACANCY", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        t.Id = (long)reader["ID"];
                        t.AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"];
                        t.OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"];
                        t.DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"];
                        t.DepartureTime = (DateTime)reader["DEPARTURE_TIME"];
                        t.LandingTime = (DateTime)reader["LANDING_TIME"];
                        t.RemainingTickets = (int)reader["REMAINING_TICKETS"];
                        vacancyFlights.Add(t,t.RemainingTickets);
                    }
                    con.Close();
                    return vacancyFlights;
                }
            }
        }

        public Flight GetFlightById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            List<Flight> flights = new List<Flight>();
            Flight t = new Flight();            
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_FLIGHTS_BY_CUSTOMER", con);
                cmd.Parameters.Add(new SqlParameter("@id", customer.Id));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        t.Id = (long)reader["ID"];
                        t.AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"];
                        t.OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"];
                        t.DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"];
                        t.DepartureTime = (DateTime)reader["DEPARTURE_TIME"];
                        t.LandingTime = (DateTime)reader["LANDING_TIME"];
                        t.RemainingTickets = (int)reader["REMAINING_TICKETS"];
                        flights.Add(t);
                    }
                    con.Close();
                    return flights;
                }
            }
        }

        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            List<Flight> flights = new List<Flight>();
            Flight t = new Flight();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_FLIGHT_BY_DEPERTURE_DATE", con);
                cmd.Parameters.Add(new SqlParameter("@departureDate", departureDate));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        t.Id = (long)reader["ID"];
                        t.AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"];
                        t.OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"];
                        t.DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"];
                        t.DepartureTime = (DateTime)reader["DEPARTURE_TIME"];
                        t.LandingTime = (DateTime)reader["LANDING_TIME"];
                        t.RemainingTickets = (int)reader["REMAINING_TICKETS"];
                        flights.Add(t);
                    }
                    con.Close();
                    return flights;
                }
            }
        }

        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            List<Flight> flights = new List<Flight>();
            Flight t = new Flight();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_FLIGHT_BY_COUNTRY_CODE", con);
                cmd.Parameters.Add(new SqlParameter("@countryCode", countryCode));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        t.Id = (long)reader["ID"];
                        t.AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"];
                        t.OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"];
                        t.DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"];
                        t.DepartureTime = (DateTime)reader["DEPARTURE_TIME"];
                        t.LandingTime = (DateTime)reader["LANDING_TIME"];
                        t.RemainingTickets = (int)reader["REMAINING_TICKETS"];
                        flights.Add(t);
                    }
                    con.Close();
                    return flights;
                }
            }
        }

        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            List<Flight> flights = new List<Flight>();
            Flight t = new Flight();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_FLIGHT_BY_LANDING_DATE", con);
                cmd.Parameters.Add(new SqlParameter("@landingDate", landingDate));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        t.Id = (long)reader["ID"];
                        t.AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"];
                        t.OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"];
                        t.DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"];
                        t.DepartureTime = (DateTime)reader["DEPARTURE_TIME"];
                        t.LandingTime = (DateTime)reader["LANDING_TIME"];
                        t.RemainingTickets = (int)reader["REMAINING_TICKETS"];
                        flights.Add(t);
                    }
                    con.Close();
                    return flights;
                }
            }
        }

        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            List<Flight> flights = new List<Flight>();
            Flight t = new Flight();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_FLIGHT_BY_ORIGIN_COUNTRY", con);
                cmd.Parameters.Add(new SqlParameter("@countryCode", countryCode));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        t.Id = (long)reader["ID"];
                        t.AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"];
                        t.OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"];
                        t.DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"];
                        t.DepartureTime = (DateTime)reader["DEPARTURE_TIME"];
                        t.LandingTime = (DateTime)reader["LANDING_TIME"];
                        t.RemainingTickets = (int)reader["REMAINING_TICKETS"];
                        flights.Add(t);
                    }
                    con.Close();
                    return flights;
                }
            }
        }

        public void Remove(Flight t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"DELETE_FLIGHT", con);
                cmd.Parameters.Add(new SqlParameter("@Id", t.Id));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(Flight t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE_FLIGHT", con);
                cmd.Parameters.Add(new SqlParameter("@ID", t.Id));
                cmd.Parameters.Add(new SqlParameter("@AIRLINECOMPANY_ID", t.AirlineCompanyId));
                cmd.Parameters.Add(new SqlParameter("@ORIGIN_COUNTRY_CODE", t.OriginCountryCode));
                cmd.Parameters.Add(new SqlParameter("@DESTINATION_COUNTRY_CODE", t.DestinationCountryCode));
                cmd.Parameters.Add(new SqlParameter("@DEPARTURE_TIME", t.DepartureTime));
                cmd.Parameters.Add(new SqlParameter("@LANDING_TIME", t.LandingTime));
                cmd.Parameters.Add(new SqlParameter("@REMAINING_TICKETS", t.RemainingTickets));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
