using FlightsManagementSystem;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FlightsWebApplication.Controllers
{    
    internal class BasicAuthenticationCustomerAttribute : AuthorizationFilterAttribute
    {
        LoggedInAdministratorFacade facade = new LoggedInAdministratorFacade();
        Administrator admin = new Administrator("admin","9999");
        LoginToken<Administrator> AdminLogin = new LoginToken<Administrator>();
        
        
        Customer customer = new Customer();
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            
            string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

            string decodedAuthenticationToken = Encoding.UTF8.GetString(
                Convert.FromBase64String(authenticationToken));

            string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
            string username = usernamePasswordArray[0];
            string password = usernamePasswordArray[1];
            try
            {
                AdminLogin.User = admin;
                customer = facade.GetCustomerByUserName(AdminLogin, username);
                if (username == customer.UserName && password == customer.Password)
                {

                    actionContext.Request.Properties["token"] = customer;

                }
                else
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized, "incorrect user or password");
                }
            }
            catch (Exception)
            {
                
            }
           
            
        }
    }
}