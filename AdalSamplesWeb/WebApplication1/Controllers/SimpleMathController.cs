using AdalSamples.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AdalSamples.WebApiClient;

namespace WebApplication1.Controllers
{
    [RequireHttps]
    [Authorize]
    [RoutePrefix("api/simplemath")]
    public class SimpleMathController : ApiController
    {

        [HttpGet]
        [CacheControl(MaxAge = 1)]
        [Route("AddNumbers/{number1}/{number2}")]
        public HttpResponseMessage AddNumbers(int number1, int number2)
        {
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;

            if (identity == null)
                throw new ApplicationException("Failed to retrieve identity");

            string upn = identity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value;

            int answer = number1 + number2;

            string result = string.Format("WebApp1 says, Hello {0}. Your answer is :{1}", upn, answer);

            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        [HttpGet]
        [CacheControl(MaxAge = 1)]
        [Route("PowerEx/{number1}/{number2}")]
        public async Task<HttpResponseMessage> PowerEx(int number1, int number2)
        {
            string instance = ConfigurationManager.AppSettings["ida:AADInstance"];
            string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
            string clientId = ConfigurationManager.AppSettings["ida:ClientID"];
            string password = ConfigurationManager.AppSettings["ida:Password"];
            string externalResource = ConfigurationManager.AppSettings["ExternalResourceId"];

            string externalResourceUri = ConfigurationManager.AppSettings["ExternalResourceUri"];

            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;

            var accessToken = await TokenHelper.GetAADAccessToken(clientId, password, instance, tenant, externalResource, identity);

            var webapi2Client = new AdvancedMathClient(externalResourceUri, accessToken);
            var result = await webapi2Client.Power(number1, number2);

            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

    }
}