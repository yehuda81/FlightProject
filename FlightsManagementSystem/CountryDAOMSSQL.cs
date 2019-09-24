using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class CountryDAOMSSQL : ICountryDAO
    {
        public void Add(Country t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                SqlCommand cmd = new SqlCommand("ADD_COUNTRY", con);
                cmd.Parameters.Add(new SqlParameter("@COUNTRY_NAME", t.CountryName));                
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public Country GetByName(string name)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            Country t = new Country();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_COUNTRY_BY_NAME", con);
                cmd.Parameters.Add(new SqlParameter("@name", name));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        t.Id = (long)reader["ID"];
                        t.CountryName = (string)reader["COUNTRY_NAME"];
                    }
                    con.Close();
                    return t;
                }
            }
        }

        public Country Get(int id)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            Country t = new Country();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_COUNTRY_BY_ID", con);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        t.Id = (long)reader["ID"];
                        t.CountryName = (string)reader["COUNTRY_NAME"];                        
                    }
                    con.Close();
                    return t;
                }
            }
        }

        public IList<Country> GetAll()
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            List<Country> countries = new List<Country>();
            Country t = new Country();
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"SELECT_ALL_COUNTRIES", con);
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        t.Id = (long)reader["ID"];
                        t.CountryName = (string)reader["COUNTRY_NAME"];
                        countries.Add(t);
                    }
                    con.Close();
                    return countries;
                }
            }
        }

        public void Remove(Country t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {               
                SqlCommand cmd = new SqlCommand($"DELETE_COUNTRY", con);
                cmd.Parameters.Add(new SqlParameter("@id", t.Id));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(Country t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE_COUNTRY", con);
                cmd.Parameters.Add(new SqlParameter("@id", t.Id));
                cmd.Parameters.Add(new SqlParameter("@COUNTRY_NAME", t.CountryName));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
