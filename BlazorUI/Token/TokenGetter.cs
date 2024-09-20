using Agro.Model.Dto.User;
using Agro.Model.WebApi.Models;
using Blazored.LocalStorage;
using System.Net.Http.Json;


namespace BlazorUI.Token;

internal class TokenGetter(HttpClient httpClient, ILocalStorageService localStorageService) : ITokenGetter
{
    public static AuthenticatedResponse TokenInfo { get; set; }
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorageService = localStorageService;

    //IConfiguration _configuration = configuration;
    string webApiRoot = "http://localhost:7272/api/Auth/login";
    //public TokenGetter(HttpClient httpClient, IConfiguration configuration)
    //{
    //    _httpClient = httpClient;
    //    _configuration = configuration;
    //}
    public async Task<AuthenticatedResponse> GetTokenInfoAsync(Login login)
    {
        //_configuration.GetValue("").
        var result = await _httpClient.PostAsJsonAsync(webApiRoot, login);
        result.EnsureSuccessStatusCode();
        var TokenInfo = await result.Content.ReadFromJsonAsync<AuthenticatedResponse>();
        await localStorageService.SetItemAsync("authToken", result);
        return TokenInfo;
    }

    public async Task<string> GetTokenAsync()
    {
        //_configuration.GetValue("").
        var tokenInfo = await localStorageService.GetItemAsync<AuthenticatedResponse>("authToken");
        return tokenInfo!.Token!;
    }
}
