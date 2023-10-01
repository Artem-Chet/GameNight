using GameNight.Client;
using GameNight.Client.PlayedGames;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("GameNight.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddMudServices();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("GameNight.ServerAPI"));

builder.Services.AddScoped<PlayedGamesClient, PlayedGamesClient>();

await builder.Build().RunAsync();