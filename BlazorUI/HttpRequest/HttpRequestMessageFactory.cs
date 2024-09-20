using Agro.Model.WebApi.Models;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BlazorUI.HttpRequest;

internal class HttpRequestMessageFactory(ILocalStorageService localStorageService)
{
    private readonly ILocalStorageService _localStorageService = localStorageService;
    //public static HttpRequestMessage Create(HttpMethod httpMethod, string authorizationHeader, string url, object body = null)
    public async Task<HttpRequestMessage> CreateAsync(HttpMethod httpMethod, bool needAuthHeader, string url, object body = null)

    {
        var httpRequestMessage = new HttpRequestMessage(httpMethod, url);
        if (needAuthHeader)
        {
            //var tokenInfo = await localStorageService.SetItemAsync("authToken", result);
            var tokenInfo = await localStorageService.GetItemAsync<AuthenticatedResponse>("authToken");
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenInfo.Token);
        }
        if (body != null)
        {
            httpRequestMessage.Content = new StringContent(
                JsonSerializer.Serialize(body),
                Encoding.UTF8, 
                "application/json"
                );
        }
        return httpRequestMessage;
    }
}