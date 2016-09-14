using AdalSamples.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    [RequireHttps]
    [Authorize]
    [RoutePrefix("api/advancedmath")]
    public class AdvancedMathController : ApiController
    {

        [HttpGet]
        [CacheControl(MaxAge = 1)]
        [Route("Power/{number1}/{number2}")]
        public HttpResponseMessage Power(double number1, double number2)
        {
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;

            if (identity == null)
                throw new ApplicationException("Failed to retrieve identity");

            string upn = identity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value;

            double answer = Math.Pow(number1,number2);

            string result = string.Format("WebApp2 says, Hello {0}. Your answer is: {1}", upn, answer);

            return Request.CreateResponse(HttpStatusCode.OK, result);

        }


    }
}