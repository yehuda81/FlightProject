using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{    
    public class AppConfig
    {
        public const string CONNECTION_STRING = @"Data Source=.;Initial Catalog=YehudaReisner;Integrated Security=True";
        public const string ADMIN_USER = "admin";
        public const string ADMIN_PASSWORD = "9999";
        public const int TIMETOMOVETOHISTORY = 12;
    }
}
