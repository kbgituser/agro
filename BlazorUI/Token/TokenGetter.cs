using Agro.Model.Dto.User;
using Agro.Model.WebApi.Models;
using System.Net.Http.Json;

namespace BlazorUI.Token;

internal class TokenGetter(HttpClient httpClient) : ITokenGetter
{
    public static AuthenticatedResponse TokenInfo { get; set; }
    private readonly HttpClient _httpClient = httpClient;
    //IConfiguration _configuration = configuration;
    string webApiRoot = "http://localhost:7272/api/Auth/login";
    //public TokenGetter(HttpClient httpClient, IConfiguration configuration)
    //{
    //    _httpClient = httpClient;
    //    _configuration = configuration;
    //}
    public async Task<AuthenticatedResponse> GetTokenAsync(Login login)
    {
        //_configuration.GetValue("").
        var result = await _httpClient.PostAsJsonAsync(webApiRoot, login);
        result.EnsureSuccessStatusCode();
        var TokenInfo = await result.Content.ReadFromJsonAsync<AuthenticatedResponse>();
        return TokenInfo;
    }
}
