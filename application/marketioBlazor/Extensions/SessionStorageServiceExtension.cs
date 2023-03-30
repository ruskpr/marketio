﻿using Blazored.SessionStorage;
using Newtonsoft.Json;
using System.Text;

namespace marketioBlazor.Extensions
{
    public static class SessionStorageServiceExtension
    {
        public static async Task SetObjectAsync<T>(this ISessionStorageService service, string key, T value)
        {
            await service.SetItemAsync(key, JsonConvert.SerializeObject(value));
        }
        public static async Task<T> GetObjectAsync<T>(this ISessionStorageService service, string key)
        {
            var value = await service.GetItemAsync<string>(key);
            if (value == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static async Task SaveItemEncryptedAsync<T>(this ISessionStorageService sessionStorageService, string key, T item)
        {
            var itemJson = JsonConvert.SerializeObject(item);
            var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson);
            var base64Json = Convert.ToBase64String(itemJsonBytes);
            await sessionStorageService.SetItemAsync(key, base64Json);
        }

        public static async Task<T> ReadEncryptedItemAsync<T>(this ISessionStorageService sessionStorageService, string key)
        {
            var base64Json = await sessionStorageService.GetItemAsync<string>(key);
            var itemJsonBytes = Convert.FromBase64String(base64Json);
            var itemJson = Encoding.UTF8.GetString(itemJsonBytes);
            var item = JsonConvert.DeserializeObject<T>(itemJson);
            return item;

        }

    }
}
