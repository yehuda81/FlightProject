using FlightsManagementSystem;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FlightsWebApplication.Controllers
{
    internal class BasicAuthenticationAdministratorAttribute : AuthorizationFilterAttribute
    {
        Administrator administrator = new Administrator(null,null);
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
                administrator.UserName = username;
                administrator.Password = password;
            }
            catch (Exception)
            {

            }
            if (username == AppConfig.ADMIN_USER && password == AppConfig.ADMIN_PASSWORD)
            {
                actionContext.Request.Properties["token"] = administrator;
            }
            else
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized, "incorrect user or password");
            }
        }
            	        
		
	}
	
}