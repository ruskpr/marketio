using MarketIO.Shared;
using MarketIO.Shared.Models;

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

        #endregion
        [Fact]
        public void Test_Login_User()
        {
            ApiClient api = new ApiClient("https://localhost:44370/api/Auth/login");

            var u = new User()
            {
                Email = "russ@gmail.com",
                PasswordHash = "123456",
            };

            var res = api.LogUserInAsync(u);

            Assert.True(res.Result.IsSuccessStatusCode);
        }
    }
}