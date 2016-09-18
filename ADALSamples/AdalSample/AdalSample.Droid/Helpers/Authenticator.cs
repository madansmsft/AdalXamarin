using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AdalSample;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;

[assembly: Dependency(typeof(AdalSample.Droid.Helpers.Authenticator))]

namespace AdalSample.Droid.Helpers
{
    class Authenticator  : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri = "https://localhost")
        {

            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, new Uri(returnUri), new PlatformParameters((Activity)Forms.Context));
            return authResult;


        }

    }
}