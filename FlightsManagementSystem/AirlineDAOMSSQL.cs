using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class AirlineDAOMSSQL : IAirlineDAO
    {
        public void Add(AirlineCompany t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                SqlCommand cmd = new SqlCommand("ADD_COMPANY", con);
                cmd.Parameters.Add(new SqlParameter("@AIRLINE_NAME", t.AirlineName));
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", t.UserName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", t.Password));
                cmd.Parameters.Add(new SqlParameter("@COUNTRY_CODE", t.CountryCode));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public AirlineCompany Get(int id)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            AirlineCompany t = new AirlineCompany();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_AIRLINECOMPANY_BY_ID", con);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {                        
                        t.Id = (long)reader["ID"];
                        t.AirlineName = (string)reader["AIRLINE_NAME"];
                        t.UserName = (string)reader["USER_NAME"];
                        t.Password = (string)reader["PASSWORD"];
                        t.CountryCode = (long)reader["COUNTRY_CODE"];
                    }
                    con.Close();
                    return t;
                }
            }
        }

        public AirlineCompany GetAirlineByUsername(string username)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            AirlineCompany t = new AirlineCompany();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_AIRLINECOMPANY_BY_USERNAME", con);
                cmd.Parameters.Add(new SqlParameter("@username", username));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {                        
                        t.Id = (long)reader["ID"];
                        t.AirlineName = (string)reader["AIRLINE_NAME"];
                        t.UserName = (string)reader["USER_NAME"];
                        t.Password = (string)reader["PASSWORD"];
                        t.CountryCode = (long)reader["COUNTRY_CODE"];
                    }
                    con.Close();
                    return t;
                }
            }
        }
        
        public IList<AirlineCompany> GetAll()
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            IList<AirlineCompany> companies = new List<AirlineCompany>();          
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_ALL_AIRLINECOMPANIES", con);                
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        AirlineCompany t = new AirlineCompany();
                        t.Id = (long)reader["ID"];
                        t.AirlineName = (string)reader["AIRLINE_NAME"];
                        t.UserName = (string)reader["USER_NAME"];
                        t.Password = (string)reader["PASSWORD"];
                        t.CountryCode = (long)reader["COUNTRY_CODE"];
                        companies.Add(t);
                    }
                    con.Close();                   
                    return companies;
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"DELETE_ALL_AIRLINESCOMPANIES", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    

        public IList<AirlineCompany> GetAllAirlinesByCountry(int countryCode)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            IList<AirlineCompany> airlines = new List<AirlineCompany>();
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"SELECT_AIRLINECOMPANIES_BY_COUNTRY_CODE", con);
                cmd.Parameters.Add(new SqlParameter("@COUNTRY_CODE", countryCode));
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        AirlineCompany t = new AirlineCompany();
                        t.Id = (long)reader["ID"];
                        t.AirlineName = (string)reader["AIRLINE_NAME"];
                        t.UserName = (string)reader["USER_NAME"];
                        t.Password = (string)reader["PASSWORD"];
                        t.CountryCode = (long)reader["COUNTRY_CODE"];
                        airlines.Add(t);
                    }
                    con.Close();
                    return airlines;
                }
            }
        }

        public void Remove(AirlineCompany t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {                
                SqlCommand cmd = new SqlCommand($"DELETE_AirlineCompanie", con);
                cmd.Parameters.Add(new SqlParameter("@id", t.Id));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }           

        public void Update(AirlineCompany t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE_AirlineCompanie",con);
                cmd.Parameters.Add(new SqlParameter("@Id", t.Id));
                cmd.Parameters.Add(new SqlParameter("@AIRLINE_NAME", t.AirlineName));
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", t.UserName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", t.Password));
                cmd.Parameters.Add(new SqlParameter("@COUNTRY_CODE", t.CountryCode));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
