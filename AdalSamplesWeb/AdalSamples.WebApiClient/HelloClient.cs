using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdalSamples.WebApiClient
{
    public class HelloClient : ClientBase
    {
        public HelloClient(string baseUri, string accessToken) : base(baseUri, accessToken) { }


        public async Task<string> SayHello()
        {
            return await GetAsync<string>("SayHello");
        }

    }
}
