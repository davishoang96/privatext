using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient("ServerAPI", client => client.BaseAddress = new Uri($"http://localhost:{builder.Configuration["PortNumber"]}"));
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
  .CreateClient("ServerAPI"));

await builder.Build().RunAsync();
