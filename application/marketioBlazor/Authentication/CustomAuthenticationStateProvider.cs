using Blazored.SessionStorage;
using Common.DTO;
using marketioBlazor.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace marketioBlazor.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await _sessionStorage.ReadEncryptedItemAsync<UserSessionDTO>("UserSession");
                if (userSession == null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Email),
                    new Claim(ClaimTypes.Role, userSession.Role),
                }, "JwtAuth"));

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserSessionDTO? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Email),
                    new Claim(ClaimTypes.Role, userSession.Role),
                }));

                userSession.ExpiryTimeStamp = DateTime.Now.AddMinutes(userSession.ExpiresIn);
                //await _sessionStorage.SaveItemEncryptedAsync("UserSession", userSession);

                try
                {
                    var itemJson = JsonConvert.SerializeObject(userSession);
                    var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson);
                    var base64Json = Convert.ToBase64String(itemJsonBytes);
                    await _sessionStorage.SetItemAsync("UserSession", base64Json);
                }
                catch 
                {
                    throw;
                }
            }
            else
            {
                claimsPrincipal = _anonymous;
                await _sessionStorage.RemoveItemAsync("UserSession");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task<string> GetToken()
        {
            var result = string.Empty;

            try
            {
                var userSession = await _sessionStorage.ReadEncryptedItemAsync<UserSessionDTO>("UserSession");
                if (userSession != null && DateTime.Now < userSession.ExpiryTimeStamp)
                {
                    result = userSession.Token;
                }
            }
            catch { }

            return result;
        }

        public async Task LogoutUser()
        {
            await UpdateAuthenticationState(null);
        }
    }

    
}
