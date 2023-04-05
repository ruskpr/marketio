using Blazored.SessionStorage;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Text;

namespace marketioBlazor.Extensions
{
    public class CookieStorageAccessor
    {
        private Lazy<IJSObjectReference> _accessorJsRef = new();
        private readonly IJSRuntime _jsRuntime;

        public CookieStorageAccessor(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        private async Task WaitForReference()
        {
            if (_accessorJsRef.IsValueCreated is false)
            {
                _accessorJsRef = new(await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/CookieStorageAccessor.js"));
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_accessorJsRef.IsValueCreated)
            {
                await _accessorJsRef.Value.DisposeAsync();
            }
        }

        #region get / set

        public async Task<T> GetValueAsync<T>(string key)
        {
            try
            {
                await WaitForReference();
                var base64Json = await _accessorJsRef.Value.InvokeAsync<string>("get", key);

                var itemJsonBytes = Convert.FromBase64String(base64Json.ToString());
                var itemJson = Encoding.UTF8.GetString(itemJsonBytes);
                var item = JsonConvert.DeserializeObject<T>(itemJson);

                return (T)item;
            }
            catch { return default; }
          
        }

        public async Task SetValueAsync<T>(string key, T value)
        {
            var itemJson = JsonConvert.SerializeObject(value);
            var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson);
            var base64Json = Convert.ToBase64String(itemJsonBytes);

            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("set", key, base64Json);
        }

        #endregion
    }
}

