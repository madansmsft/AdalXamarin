using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdalSamples.WebApiClient
{
    public class AdvancedMathClient : ClientBase
    {
        public AdvancedMathClient(string baseUri, string accessToken) : base(baseUri, accessToken) { }

        public async Task<string> Power(int number1, int number2)
        {
            return await GetAsync<string>("Power", number1, number2);
        }


    }
}
