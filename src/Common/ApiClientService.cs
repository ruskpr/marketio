using System.Net;
using System.Reflection;
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
        private const string DEFAULT_ENDPOINT = "http://localhost:7244/";
        //private const string DEFAULT_ENDPOINT = "http://172.18.31.108:7244/";

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

        /// <summary>
        /// 
        /// Send 'RegisterDTO' object to api,
        /// if successful, the api will return add user to database
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// This method will get all items from a table
        /// 
        /// </summary>
        public async Task<List<T>> GetAsync<T>(string? controllerName = null) where T : IDbModel
        {
            List<T>? ret = new List<T>();

            string endpoint = controllerName ?? typeof(T).Name + "s";

            var request = new RestRequest($"api/{endpoint}");

            var response = await _client.GetAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                ret = JsonConvert.DeserializeObject<List<T>>(response.Content);
            }

            return ret;
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : IDbModel
        {
            var request = new RestRequest($"api/{typeof(T).Name}s/{id}");

            var response = await _client.GetAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }

            return default;
        }

        public async Task<List<Message>> GetMessagesByTransactionId(int transactionId)
        {
            var request = new RestRequest($"api/Messages");
            var response = await _client.GetAsync(request);

            List<Message> messages = JsonConvert.DeserializeObject<List<Message>>(response.Content) ?? new List<Message>();

            return messages.Where(m => m.TransactionId == transactionId).ToList();
        }

        public async Task<List<UserRating>> GetUserRatingByUserId(int userId)
        {
            var request = new RestRequest($"api/UserRatings");
            var response = await _client.GetAsync(request);
            List<UserRating> userRatings = JsonConvert.DeserializeObject<List<UserRating>>(response.Content) ?? new List<UserRating>();
            return userRatings.Where(m => m.UserId == userId).ToList();
        }

        #endregion

        #region post

        public async Task<RestResponse> PostAsync<T>(IDbModel model, string? controllerName = null) where T : IDbModel
        {
            var modelAsJson = JsonConvert.SerializeObject(model);
            RestResponse response;
            string endpoint = controllerName ?? typeof(T).Name + "s";

            var request = new RestRequest($"api/{endpoint}").AddStringBody(modelAsJson, ContentType.Json);
            //var request = new RestRequest($"api/{endpoint}").AddJsonBody(model);

            try
            {
                response = await _client.PostAsync(request);
            }
            catch (HttpRequestException)
            {
                throw;
            }

            return response;
        }

        #endregion

        #region put

        /// <summary>
        /// 
        /// Generic method that is able to update and IDbModel object
        /// 
        /// </summary>
        public async Task<RestResponse> PutAsync<T>(T model, string? controllerName = null) where T : IDbModel
        {
            var modelAsJson = JsonConvert.SerializeObject(model);


            string endpoint = controllerName ?? typeof(T).Name + "s";
            var request = new RestRequest("api/"+endpoint+$"/{model.Id}").AddStringBody(modelAsJson, ContentType.Json);
            RestResponse response;
            try
            {
                response = await _client.ExecutePutAsync(request);
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        #endregion

        #region delete

        /// <summary>
        /// 
        /// Deletes the item with the given id
        /// 
        /// </summary>
        public async Task<RestResponse> DeleteAsync<T>(int id, string? controllerName = null) where T : IDbModel
        {
            string endpoint = controllerName ?? typeof(T).Name + "s";
            var request = new RestRequest($"api/{endpoint}/{id}");
            return await _client.DeleteAsync(request);
        }

        #endregion

    }
}