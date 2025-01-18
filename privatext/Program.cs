using FastEndpoints;
using FastEndpoints.ClientGen;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NJsonSchema.CodeGeneration.CSharp;
using privatext.Components;
using privatext.Database;
using privatext.Services;
using Radzen;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<DatabaseContext>();
builder.Services.AddFastEndpoints().SwaggerDocument(o =>
{
    o.ShortSchemaNames = true; // prevent adding namespace as prefix to classes.
    o.DocumentSettings = s => s.DocumentName = "MyAPI"; //must match doc name below
});
builder.Services.AddEndpointsApiExplorer();

// Services
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddSingleton<IRandomString, RandomString>();
builder.Services.AddSingleton<ICryptoService, CryptoService>();

// Add services to the container.
builder.Services.AddRadzenComponents();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<IApiClient>(sp =>
       new ApiClient(builder.Configuration["BaseUrl"],
       new HttpClient { BaseAddress = new Uri(builder.Configuration["BaseUrl"]) }));

var app = builder.Build();

// Apply migration
using (var scope = ((IApplicationBuilder)app).ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<DatabaseContext>().Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    //app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionHandler(errApp =>
{
    errApp.Run(async ctx =>
    {
        var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();
        if (exHandlerFeature != null)
        {
            var reason = exHandlerFeature.Error.Message;
            string errorMessage = reason;
            if (errorMessage.Contains("ThrowError() called! - "))
            {
                errorMessage = errorMessage.Replace("ThrowError() called! - ", string.Empty);
            }

            ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ctx.Response.ContentType = "application/problem+json";
            await ctx.Response.WriteAsJsonAsync(
            new ErrorResponse
            {
                StatusCode = ctx.Response.StatusCode,
                Message = errorMessage,
            });
        }
    });
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode()
   .AddInteractiveWebAssemblyRenderMode()
   .AddAdditionalAssemblies(typeof(privatext.Client._Imports).Assembly);

app.UseFastEndpoints(c =>
{
    c.Endpoints.ShortNames = true;
    c.Serializer.Options.PropertyNamingPolicy = null;
}).UseSwaggerGen();

await app.GenerateClientsAndExitAsync(
    documentName: "MyAPI",
    destinationPath: "../privatext.Services/",
    csSettings: c =>
    {
        c.ClassName = "ApiClient";
        c.InjectHttpClient = true;
        c.GenerateClientInterfaces = true;
        c.GenerateDtoTypes = false;
        c.CSharpGeneratorSettings.Namespace = "privatext.Services";
        c.AdditionalNamespaceUsages = ["privatext.Common.DTO", "privatext.Common.Request", "privatext.Common.Response"];
        c.CSharpGeneratorSettings.JsonLibrary = CSharpJsonLibrary.NewtonsoftJson;
    },
    tsSettings: null);

app.Run();
