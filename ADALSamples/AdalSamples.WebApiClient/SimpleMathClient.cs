using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdalSamples.WebApiClient
{
    public class SimpleMathClient : ClientBase
    {
        public SimpleMathClient(string baseUri, string accessToken) : base(baseUri, accessToken) { }

        public async Task<string> AddNumbers(int number1, int number2)
        {
            return await GetAsync<string>("AddNumbers", number1, number2);
        }

        public async Task<string> PowerEx(int number1, int number2)
        {
            return await GetAsync<string>("PowerEx", number1, number2);
        }

    }
}
