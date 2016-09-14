using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Linq;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AdalSample.iOS.Helpers.Authenticator))]

namespace AdalSample.iOS.Helpers
{
    public class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController);

            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);

            return authResult;
        }
    }
}
