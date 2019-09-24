using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class Administrator : IUser
    {
        public  string UserName;
        public  string Password;

        public Administrator(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
