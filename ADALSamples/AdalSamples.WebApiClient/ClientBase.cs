using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdalSamples.WebApiClient
{
    public abstract class ClientBase
    {
        private HttpClient _client;
        private string _accessToken;
        private string _baseUri;

        public ClientBase(string baseUri, string accessToken)
        {
            if (_client == null)
            {
                _baseUri = baseUri;
                _accessToken = accessToken;

                _client = new HttpClient();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            }
        }


        protected async Task<T> GetAsyncWithQueryString<T>(string actionName, Dictionary<string, string> queryString, params object[] parameters)
        {
            HttpResponseMessage response = null;


            string fullPath = string.Format("{0}/api/{2}/{1}/", _baseUri, actionName.ToLowerInvariant(), this.GetType().Name.ToLowerInvariant().Replace("client", ""));// _baseUri + "api/" +  methodName.ToLowerInvariant this.GetType().Name.ToLowerInvariant().Replace("client", "") + "/";

            foreach (var item in parameters)
            {

                fullPath += WebUtility.UrlEncode(item == null ? "null" : item.ToString()) + "/";
            }



            string qs = "";

            foreach (var item in queryString)
                qs += string.Format("{0}={1}&", item.Key, WebUtility.UrlEncode(item.Value));


            fullPath = fullPath + "?" + qs;

            response = await _client.GetAsync(fullPath);
            var result = await response.Content.ReadAsStringAsync(); //there is no ReadAsAsync<> implementation in this httpclient

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(result);
            }


            return JsonConvert.DeserializeObject<T>(result, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
        }



        protected async Task<T> GetAsync<T>(string actionName, params object[] parameters)
        {
            HttpResponseMessage response = null;


            string fullPath = string.Format("{0}/api/{2}/{1}/", _baseUri, actionName.ToLowerInvariant(), this.GetType().Name.ToLowerInvariant().Replace("client", ""));// _baseUri + "api/" +  methodName.ToLowerInvariant this.GetType().Name.ToLowerInvariant().Replace("client", "") + "/";

            foreach (var item in parameters)
            {

                fullPath += WebUtility.UrlEncode(item == null ? "null" : item.ToString()) + "/";
            }

            fullPath = fullPath.Replace("+", "%20");

            response = await _client.GetAsync(fullPath);
            var result = await response.Content.ReadAsStringAsync(); //there is no ReadAsAsync<> implementation in this httpclient

            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(result);
            }


            return JsonConvert.DeserializeObject<T>(result, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
        }

        protected async Task<T> PostAsync<T>(string actionName, object body = null, params object[] parameters)
        {
            HttpResponseMessage response = null;

            if (body == null)
                body = string.Empty;

            string fullPath = string.Format("{0}/api/{2}/{1}/", _baseUri, actionName.ToLowerInvariant(), this.GetType().Name.ToLowerInvariant().Replace("client", ""));// _baseUri + "api/" +  methodName.ToLowerInvariant this.GetType().Name.ToLowerInvariant().Replace("client", "") + "/";

            foreach (var item in parameters)
                fullPath += WebUtility.UrlEncode(item == null ? "null" : item.ToString()) + "/";


            var requestContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");


            response = await _client.PostAsync(fullPath, requestContent);
            var result = await response.Content.ReadAsStringAsync(); //there is no ReadAsAsync<> implementation in this httpclient

            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(result);
            }

            return JsonConvert.DeserializeObject<T>(result, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
        }
    }
}
