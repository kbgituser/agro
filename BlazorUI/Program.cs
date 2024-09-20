using Blazored.LocalStorage;
using BlazorUI;
using BlazorUI.HttpRequest;
using BlazorUI.Token;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<TokenGetter>();
builder.Services.AddScoped<HttpRequestMessageFactory>();
builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddSingleton<ITokenGetter, TokenGetter>( );


await builder.Build().RunAsync();
