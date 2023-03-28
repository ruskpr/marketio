using Common.DTO;
using Common.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ApiClientService
    {
        private RestClient _client;

        public ApiClientService(string endpointUrl)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(endpointUrl),
            };

            _client = new RestClient(httpClient);
        }

        public ApiClientService()
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
            var bodyContent = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");
            var request = new RestRequest("api/Auth/login").AddJsonBody(bodyContent);
            var response = _client.Post(request);

            return response;
        }

        // create a method to register user
        public RestResponse RegisterUser(RegisterDTO regDTO)
        {
            var bodyContent = new StringContent(JsonConvert.SerializeObject(regDTO),
                              Encoding.UTF8, "application/json");
            var request = new RestRequest("api/Auth/register").AddJsonBody(bodyContent);
            var response = _client.Post(request);

            return response;
        }

        public async Task<RestResponse> RegisterUserAsync(RegisterDTO regDTO)
        {
            var bodyContent = new StringContent(JsonConvert.SerializeObject(regDTO),
                                             Encoding.UTF8, "application/json");
            var request = new RestRequest("api/Auth/register").AddJsonBody(bodyContent);
            var response = await _client.PostAsync(request);
            return response;
        }

        #region get

        // create a method to get all users
        public async Task<List<T>>? GetAllAsync<T>() where T : IDbModel
        {
            List<T>? ret = new List<T>();

            var request = new RestRequest($"api/{typeof(T).Name}s");

            var response = await _client.GetAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                ret = JsonConvert.DeserializeObject<List<T>>(response.Content);
            }

            return ret;
        }

        public async Task<T?> GetByIdAsync<T>(int id) where T : IDbModel
        {
            var request = new RestRequest($"api/{typeof(T).Name}s/{id}");

            var response = await _client.GetAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }

            return default(T?);
        }

        #endregion

        #region post

        public async Task<RestResponse> CreateAsync<T>(T model) where T : IDbModel
        {
            var bodyContent = new StringContent(JsonConvert.SerializeObject(model),
                                             Encoding.UTF8, "application/json");

            var request = new RestRequest($"api/{typeof(T).Name}s").AddJsonBody(bodyContent);

            var response = await _client.PostAsync(request);

            return response;
        }

        #endregion

        #region put

        // create a generic method to update any object
        public async Task<RestResponse> UpdateAsync<T>(T obj) where T : IDbModel
        {
            var bodyContent = new StringContent(JsonConvert.SerializeObject(obj),
                                              Encoding.UTF8, "application/json");

            var request = new RestRequest($"api/{typeof(T).Name}/{obj.Id}").AddJsonBody(bodyContent);

            var response = await _client.PutAsync(request);

            return response;
        }

        #endregion


    }
}