using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using privatext.Client.HttpClient;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IApiClient>(sp =>
        new ApiClient(builder.Configuration["BaseUrl"],
        new HttpClient { BaseAddress = new Uri(builder.Configuration["BaseUrl"]) }));
builder.Services.AddRadzenComponents();
await builder.Build().RunAsync();
