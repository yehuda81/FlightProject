using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class TicketDAOMSSQL : ITicketDAO
    {        
        public void Add(Ticket t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {                              
                SqlCommand cmd = new SqlCommand("ADD_TICKET", con);                              
                cmd.Parameters.Add(new SqlParameter("@FLIGHT_ID", t.FlightId));
                cmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", t.CustomerId));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        internal void MoveTicketsToTicketHistory()
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                SqlCommand cmd = new SqlCommand("MOVE_TICKETS_TO_HISTORY", con);
                cmd.Parameters.Add(new SqlParameter("@Trheehoursago", DateTime.Now.AddHours(-3)));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Ticket Get(int id)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            Ticket t = new Ticket();
            using (con)
            {                
                SqlCommand cmd = new SqlCommand("SELECT_TICKETS_BY_ID", con);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read())
                {
                    t.Id = (long)reader["ID"];
                    t.FlightId = (long)reader["FLIGHT_ID"];
                    t.CustomerId = (long)reader["CUSTOMER_ID"];
                }
                con.Close();
                return t;                
            }
                
        }

        public IList<Ticket> GetAll()
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            List<Ticket> tickets = new List<Ticket>();
            using (con)
            {               
                SqlCommand cmd = new SqlCommand($"SELECT_ALL_TICKETS", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                {
                    while (reader.Read())
                    {
                        Ticket t = new Ticket();
                        t.Id = (long)reader["ID"];
                        t.FlightId = (long)reader["FLIGHT_ID"];
                        t.CustomerId = (long)reader["CUSTOMER_ID"];
                        tickets.Add(t);                        
                    }
                con.Close();
                return tickets;
                }
            }
        }

        public void Remove(Ticket t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {                
                SqlCommand cmd = new SqlCommand($"DELETE_TICKET", con);
                cmd.Parameters.Add(new SqlParameter("@id", t.Id));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();                
                con.Close();
            }
            
        }

        public void Update(Ticket t)
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                SqlCommand cmd = new SqlCommand($"UPDATE_TICKET", con);
                cmd.Parameters.Add(new SqlParameter("@id", t.Id));
                cmd.Parameters.Add(new SqlParameter("@FLIGHT_ID", t.FlightId));
                cmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", t.CustomerId));
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
