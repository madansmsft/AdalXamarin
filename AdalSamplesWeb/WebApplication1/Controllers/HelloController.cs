using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using AdalSamples.Helpers;

namespace WebApplication1.Controllers
{
    [RequireHttps]
    [Authorize]
    [RoutePrefix("api/hello")]
    public class HelloController : ApiController
    {

        [HttpGet]
        [CacheControl(MaxAge = 1)]
        [Route("SayHello")]
        public HttpResponseMessage SayHello()
        {
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;

            if (identity == null)
                throw new ApplicationException("Failed to retrieve identity");

            string upn = identity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;

            string result = string.Format("Hello {0} from WebApp1", upn);

            return Request.CreateResponse(HttpStatusCode.OK, result);


        }

    }
}