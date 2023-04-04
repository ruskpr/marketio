using System.Net;
using System.Text;
using Common.DTO;
using Common.Helpers;
using Common.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Common
{
    public class ApiClientService
    {

        private RestClient _client;
        private const string DEFAULT_ENDPOINT = "https://localhost:7244/";

        #region constructors

        // default constructor
        public ApiClientService()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(DEFAULT_ENDPOINT);

            _client = new RestClient(httpClient);
        }

        // overload that takes endpoint url as parameter
        public ApiClientService(string endpointUrl)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(endpointUrl);

            _client = new RestClient(httpClient);
        }

        #endregion

        /// <summary>
        /// 
        /// If the user successfully logs in, 
        /// the HTTP response will return a UserSessionDTO object
        /// as JSON that includes a valid JWT token. 
        /// 
        /// </summary>
        public async Task<RestResponse> LogUserInAsync(LoginDTO u)
        {
            // hash password
            var loginDTO = new LoginDTO
            {
                Email = u.Email,
                PasswordHash = SHA256.Hash(u.PasswordHash)
            };

            // send request to api
            var request = new RestRequest("api/Auth/login").AddJsonBody(loginDTO);

            var response = await _client.PostAsync(request);

            return response;
        }

        public async Task<RestResponse> RegisterUserAsync(RegisterDTO regDTO)
        {
            var newUser = new RegisterDTO()
            {
                Email = regDTO.Email,
                FirstName = regDTO.FirstName,
                LastName = regDTO.LastName,
                PasswordHash = SHA256.Hash(regDTO.PasswordHash),
                ConfirmPasswordHash = SHA256.Hash(regDTO.ConfirmPasswordHash),
                RegisterDate = DateTime.Now,
            };

            var request = new RestRequest("api/Auth/register").AddJsonBody(newUser);

            var response = await _client.PostAsync(request);

            return response;
        }

        #region get

        // create a method to get all users
        public async Task<List<T>> GetAllAsync<T>() where T : IDbModel
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

        public async Task<T?> GetByIdAsync<T>(T obj) where T : IDbModel
        {
            var request = new RestRequest($"api/{typeof(T).Name}s/{obj.Id}");

            var response = await _client.GetAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }

            return default;
        }

        #endregion

        #region post

        public async Task<RestResponse> CreateAsync<T>(T model, string? apiEnpointName = null) where T : IDbModel
        {
            var modelAsJson = JsonConvert.SerializeObject(model);

            string endpoint = apiEnpointName ?? typeof(T).Name + "s";

            var request = new RestRequest($"api/{endpoint}").AddStringBody(modelAsJson, ContentType.Json);

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