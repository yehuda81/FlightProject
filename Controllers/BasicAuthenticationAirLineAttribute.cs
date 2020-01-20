using FlightsManagementSystem;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FlightsWebApplication.Controllers
{
    internal class BasicAuthenticationAirLineAttribute : AuthorizationFilterAttribute
    {
        LoggedInAirlineFacade facade = new LoggedInAirlineFacade();
        LoginToken<AirlineCompany> loginToken = new LoginToken<AirlineCompany>();
        AirlineCompany airline = new AirlineCompany();        
    
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden,
                    "You must send user name and password in basic authentication");
                return;
            }

            string decodedAuthenticationToken = Encoding.UTF8.GetString(
                Convert.FromBase64String(authenticationToken));

            string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
            string username = usernamePasswordArray[0];
            string password = usernamePasswordArray[1];
            
            try
            {
                foreach (var company in facade.GetAllAirlineCompanies())
                {
                    if (company.UserName == username && company.Password == password )
                    {
                        airline = company;
                        actionContext.Request.Properties["token"] = airline;
                        return;
                    }
                }
                
                    actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized, "incorrect user or password"); 
            } 
            catch (Exception)
            {

            }
            
        }
    }
}