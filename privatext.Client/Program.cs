using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using privatext.Client.HttpClient;
using privatext.Services;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<IRandomString, RandomString>();
builder.Services.AddSingleton<ICryptoService, CryptoService>();
builder.Services.AddScoped<IApiClient>(sp =>
        new ApiClient(builder.Configuration["BaseUrl"],
        new HttpClient { BaseAddress = new Uri(builder.Configuration["BaseUrl"]) }));
builder.Services.AddRadzenComponents();
await builder.Build().RunAsync();
