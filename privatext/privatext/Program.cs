using privatext.Components;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(o => o.ListenLocalhost(int.Parse(builder.Configuration["PortNumber"]), builder =>
{
    builder.UseHttps();
}));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("ExternalAPI", client => client.BaseAddress = new Uri($"https://localhost:{builder.Configuration["PortNumber"]}"));
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
  .CreateClient("ExternalAPI"));

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(privatext.Client._Imports).Assembly);

app.Run();
