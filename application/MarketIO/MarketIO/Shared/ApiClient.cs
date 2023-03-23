using MarketIO.Shared.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketIO.Shared
{
    public class ApiClient
    {
        private RestClient _client;
        private string _endpoint;

        public ApiClient(string endpoint)
        {
            _endpoint = endpoint;
            _client = new RestClient(endpoint);
        }

        public async Task<RestResponse> LogUserInAsync(User u)
        {
            var bodyContent = new StringContent(JsonConvert.SerializeObject(u),
                               Encoding.UTF8, "application/json");
            var request = new RestRequest("address/update").AddJsonBody(bodyContent);
            var response = await _client.PostAsync(request);

            //var payload = new StringContent(JsonConvert.SerializeObject(u),
            //    Encoding.UTF8, "application/json");

            //HttpResponseMessage response = await _client.PostAsync("/api/Auth/login", payload);

            return response;
        }


    }
}
