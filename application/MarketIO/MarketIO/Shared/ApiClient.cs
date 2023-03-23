using MarketIO.Shared.DTO;
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

        public ApiClient(string endpointUrl)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(endpointUrl),
            };

            _client = new RestClient(httpClient);
        }

        public ApiClient()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7031/"),
            };

            _client = new RestClient(httpClient);
        }

        public async Task<RestResponse> LogUserInAsync(User u)
        {
            var bodyContent = new StringContent(JsonConvert.SerializeObject(u),
                               Encoding.UTF8, "application/json");
            var request = new RestRequest("api/Auth/login").AddJsonBody(bodyContent);
            var response = await _client.PostAsync(request);

            //var payload = new StringContent(JsonConvert.SerializeObject(u),
            //    Encoding.UTF8, "application/json");

            //HttpResponseMessage response = await _client.PostAsync("/api/Auth/login", payload);

            return response;
        }

        public RestResponse LogUserIn(User u)
        {
            var bodyContent = new StringContent(JsonConvert.SerializeObject(u),
                               Encoding.UTF8, "application/json");
            var request = new RestRequest("api/Auth/login").AddJsonBody(bodyContent);
            var response = _client.Post(request);

            return response;
        }

        // create a method to register user
        public async Task<RestResponse> RegisterUserAsync(User u)
        {
            throw new NotImplementedException();
        }

        public RestResponse RegisterUser(RegisterDTO regDTO)
        {
            var bodyContent = new StringContent(JsonConvert.SerializeObject(regDTO),
                              Encoding.UTF8, "application/json");
            var request = new RestRequest("api/Auth/register").AddJsonBody(bodyContent);
            var response = _client.Post(request);

            return response;
        }
    }
}