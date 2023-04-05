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
        private CookieStorageAccessor _cookieStorageAccessor;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        private const string COOKIE_NAME = "marketio-jwt-auth-token";

        // dependency injection to get the session storage service and cookie storage service
        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorage, CookieStorageAccessor cookieStorageAccessor)
        {
            _sessionStorage = sessionStorage;
            _cookieStorageAccessor = cookieStorageAccessor;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                //// try to get token from session storage
                var userSession = await _sessionStorage.ReadEncryptedItemAsync<UserSessionDTO>(COOKIE_NAME);

                // if no token in session storage, try to get from cookie
                if (userSession == null)
                {
                    userSession = await _cookieStorageAccessor.GetValueAsync<UserSessionDTO>(COOKIE_NAME);
                }

                //userSession = await _cookieStorageAccessor.GetValueAsync<UserSessionDTO>(COOKIE_NAME);

                // if still null return un-authenticated user
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

                try
                {
                    // set cookie
                    await _cookieStorageAccessor.SetValueAsync(COOKIE_NAME, userSession);
                    // set session
                    await _sessionStorage.SaveItemEncryptedAsync(COOKIE_NAME, userSession);
                }
                catch { }
            }
            else
            {
                // clear session and cookie
                claimsPrincipal = _anonymous;
                await _cookieStorageAccessor.SetValueAsync(COOKIE_NAME, string.Empty);
                await _sessionStorage.RemoveItemAsync(COOKIE_NAME);
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task<string> GetToken()
        {
            var result = string.Empty;

            try
            {
                var userSession = await _sessionStorage.ReadEncryptedItemAsync<UserSessionDTO>(COOKIE_NAME);
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
