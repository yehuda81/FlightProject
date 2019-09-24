using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightsManagementSystem;

namespace TestFlightProject
{
    public class TestCenter
    {
        public LoginToken<AirlineCompany> AirLineToken = new LoginToken<AirlineCompany>();

        public void DeleteAllData()
        {
            SqlConnection con = new SqlConnection(AppConfig.CONNECTION_STRING);
            using (con)
            {
                SqlCommand cmd = new SqlCommand("DELETE_ALL_DB", con);                              
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.ExecuteNonQuery();                
                con.Close();
            }
        }
        public void FillData()
        {
            //LoggedInAirlineFacade AirlineFacade = new LoggedInAirlineFacade();
            //AirlineFacade.GetAllFlights();
            
            
        }
    }
    
}
