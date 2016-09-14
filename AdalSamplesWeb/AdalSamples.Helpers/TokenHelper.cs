using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdalSamples.Helpers
{
    public class TokenHelper
    {
        public static async Task<string> GetAADAccessToken(string clientId, string appKey, string aadInstance, string tenant, string resource, ClaimsIdentity identity = null)
        {
            var assertion = string.Empty;
            var userName = string.Empty;

            if (string.IsNullOrEmpty(appKey))
            {
                throw new ArgumentException("AppKey can't be blank");
            }

            if (string.IsNullOrEmpty(aadInstance))
            {
                throw new ArgumentException("aadInstance can't be blank");
            }

            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException("ClientId can't be blank");
            }

            if (string.IsNullOrEmpty(tenant))
            {
                throw new ArgumentException("tenant can't be blank");
            }

            if (string.IsNullOrEmpty(resource))
            {
                throw new ArgumentException("resource can't be blank");
            }

            //if ClaimIdentity is null lets see if we get identity from ClaimsPrincipal
            if (identity == null)
            {
                //check if user is authenticated              
                if (!ClaimsPrincipal.Current.Identity.IsAuthenticated)
                {
                    throw new ArgumentException("UnAuthenticated User");
                }

                var bootstrapContext = ClaimsPrincipal.Current.Identities.First().BootstrapContext as System.IdentityModel.Tokens.BootstrapContext;

                //check if bootstrapcontext exists
                if (bootstrapContext == null)
                {
                    throw new ArgumentException("BootstrapContext cannot be null, either token has expired or token in not saved.");
                }

                assertion = bootstrapContext.Token;
                userName = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Upn) != null ? ClaimsPrincipal.Current.FindFirst(ClaimTypes.Upn).Value : ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value;
            }
            else
            {
                //check if user is authenticated
                if (!identity.IsAuthenticated)
                {
                    throw new ArgumentException("UnAuthenticated User");
                }

                //check if bootstrapcontext exists
                if (identity.BootstrapContext == null)
                {
                    throw new ArgumentException("BootstrapContext cannot be null, either token has expired or token in not saved.");
                }

                var bootstrapContext = identity.BootstrapContext as System.IdentityModel.Tokens.BootstrapContext;
                assertion = bootstrapContext.Token;
                userName = identity.Name;
            }
            ClientCredential clientCredential = new ClientCredential(clientId, appKey);
            UserAssertion userAssertion = new UserAssertion(assertion, "urn:ietf:params:oauth:grant-type:jwt-bearer", userName);
            string authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);
            AuthenticationContext authContext = new AuthenticationContext(authority, null);
            var authResult = await authContext.AcquireTokenAsync(resource, clientCredential, userAssertion);
            return authResult.AccessToken;
        }

    }
}
