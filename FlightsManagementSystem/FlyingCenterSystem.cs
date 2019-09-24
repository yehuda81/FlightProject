using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class FlyingCenterSystem
    {        
        private static FlyingCenterSystem _instance = null;
        LoginToken<Administrator> administratorToken = new LoginToken<Administrator>();
        static object key = new object();
        bool moveToHistoryToday = false;
        Thread moveToHistory = new Thread(()=>
        {
            new TicketDAOMSSQL().MoveTicketsToTicketHistory();
            new FlightDAOMSSQL().MoveFlightsToFlightstHistory();
        });        
        public FlyingCenterSystem()
        {
            if (DateTime.Now.Hour >= AppConfig.TIMETOMOVETOHISTORY)
            {
                if (moveToHistoryToday == false)
                {
                    moveToHistory.Start();
                    moveToHistoryToday = true;
                }               
            }
            else
            {
                moveToHistoryToday = false;
            }    
           
        }
        public static FlyingCenterSystem GetInstance()
        {
            if (_instance == null)
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new FlyingCenterSystem();
                    }
                }
            }            
            return _instance;
        }        
       
        public FacadeBase GetFacade(string user, string password, out ILoginToken token)
        {
            LoginService loginService = new LoginService();            
            
            if (loginService.TryAdminLogin(user, password, out LoginToken<Administrator> AdminToken))
            {
                token = AdminToken;              
                return new LoggedInAdministratorFacade();
            }
            else if (loginService.TryAirlineLogin(user, password, out LoginToken<AirlineCompany> AirLineToken))
            {
                token = AirLineToken;
                return new LoggedInAirlineFacade();
            }
            else if (loginService.TryCustomerLogin(user, password, out LoginToken<Customer> CustomerToken))
            {
                token = CustomerToken;
                return new LoggedInCustomerFacade();
            }
            token = null;
            return new AnonymousUserFacade();            
        }
    }
    
}
