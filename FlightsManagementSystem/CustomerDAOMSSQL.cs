using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class CustomerDAOMSSQL : ICustomerDAO
    {
        public void Add(Customer t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                SqlCommand cmd = new SqlCommand("ADD_CUSTOMER", con);
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", t.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", t.LastName));
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", t.UserName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", t.Password));
                cmd.Parameters.Add(new SqlParameter("@ADDRESS", t.Address));
                cmd.Parameters.Add(new SqlParameter("@PHONE_NO", t.PhoneNo));
                cmd.Parameters.Add(new SqlParameter("@CREDIT_CARD_NUMBER", t.CreditCardNumber));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Customer Get(int id)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            Customer t = new Customer();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_CUSTOMER_BY_ID", con);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {                        
                        t.Id = (long)reader["ID"];
                        t.FirstName = (string)reader["FIRST_NAME"];
                        t.UserName = (string)reader["USER_NAME"];
                        t.LastName = (string)reader["LAST_NAME"];
                        t.Password = (string)reader["PASSWORD"];
                        t.Address = (string)reader["ADDRESS"];
                        t.PhoneNo = (string)reader["PHONE_NO"];
                        t.CreditCardNumber = (string)reader["CREDIT_CARD_NUMBER"];
                    }
                    con.Close();
                    return t;
                }
            }
        }

        public IList<Customer> GetAll()
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            List<Customer> customers = new List<Customer>();            
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"SELECT_ALL_CUSTOMERS", con);
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        Customer t = new Customer();
                        t.Id = (long)reader["ID"];
                        t.FirstName = (string)reader["FIRST_NAME"];
                        t.UserName = (string)reader["USER_NAME"];
                        t.LastName = (string)reader["LAST_NAME"];
                        t.Password = (string)reader["PASSWORD"];
                        t.Address = (string)reader["ADDRESS"];
                        t.PhoneNo = (string)reader["PHONE_NO"];
                        t.CreditCardNumber = (string)reader["CREDIT_CARD_NUMBER"];
                        customers.Add(t);
                    }
                    con.Close();
                    return customers;
                }
            }
        }

        public Customer GetCustomerByUserame(string username)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            Customer t = new Customer();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT_CUSTOMER_BY_USERNAME", con);
                cmd.Parameters.Add(new SqlParameter("@username", username));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        t.Id = (long)reader["ID"];
                        t.FirstName = (string)reader["FIRST_NAME"];
                        t.UserName = (string)reader["USER_NAME"];
                        t.LastName = (string)reader["LAST_NAME"];
                        t.Password = (string)reader["PASSWORD"];
                        t.Address = (string)reader["ADDRESS"];
                        t.PhoneNo = (string)reader["PHONE_NO"];
                        t.CreditCardNumber = (string)reader["CREDIT_CARD_NUMBER"];
                    }
                    con.Close();
                    return t;
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"DELETE_ALL_CUSTOMERS", con);                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Remove(Customer t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"DELETE_CUSTOMER", con);
                cmd.Parameters.Add(new SqlParameter("@id", t.Id));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(Customer t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE_CUSTOMER", con);
                cmd.Parameters.Add(new SqlParameter("@ID", t.Id));
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", t.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", t.LastName));
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", t.UserName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", t.Password));
                cmd.Parameters.Add(new SqlParameter("@ADDRESS", t.Address));
                cmd.Parameters.Add(new SqlParameter("@PHONE_NO", t.PhoneNo));
                cmd.Parameters.Add(new SqlParameter("@CREDIT_CARD_NUMBER", t.CreditCardNumber));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
