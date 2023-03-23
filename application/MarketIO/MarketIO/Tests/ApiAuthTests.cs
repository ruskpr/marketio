using MarketIO.Shared;
using MarketIO.Shared.DTO;
using MarketIO.Shared.Helpers;
using MarketIO.Shared.Models;
using RestSharp;

namespace MarketIO.Tests
{
    public class ApiAuthTests
    {
        //*****************
        //
        // Note: The tests will fail if the server is not running
        //
        // To run the tests properly, you must start the project with debugging
        //
        // Option 1: CTRL + F5 (Start without debugging)
        // Option 2: right-click the project 'MarketIO.Server' > Debug > Start Without Debugging
        //
        // then you run tests in test explorer
        //
        //******************


        #region login

        [Fact]
        public void Test_Login()
        {
            ApiClient api = new ApiClient("https://localhost:7031/");

            var u = new User()
            {
                Email = "russ@gmail.com",
                PasswordHash = "123456",
            };

            RestResponse res = api.LogUserIn(u);

            Assert.True(res.IsSuccessStatusCode);

            //Assert.Equal("", res.Content);
        }

        #endregion

        // create test method to register user
        #region register
        [Fact]
        public void Test_Register()
        {
            ApiClient api = new ApiClient("https://localhost:7031");

            var user = new RegisterDTO
            {
                Email = "russ@email.com",
                FirstName = "russ",
                LastName = "koprulu",
                PasswordHash = SHA256.Hash("password"),
                ConfirmPasswordHash = SHA256.Hash("password"),
            };

            RestResponse res = api.RegisterUser(user);

            Assert.True(res.IsSuccessful);
        }

        #endregion

    }
}