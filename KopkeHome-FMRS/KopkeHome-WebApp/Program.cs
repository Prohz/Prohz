using KopkeHome_UtilityLayer;
using KopkeHome_UtilityLayer.ExceptionHandler;
using System.Net.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Serilog;
using Microsoft.Extensions.Logging;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
// Ignore SSL errors
ServicePointManager.ServerCertificateValidationCallback =
delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
{
    return true; // Always accept
};
//IApplicationBuilder configuration = app.Configure;


// using serilog.
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Debug);
builder.Logging.AddFilter("System", LogLevel.Debug);
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information);


builder.Services.AddCors();
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddMvcCore();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// add localization
//builder.Services.AddSingleton<LocService>();
//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// 1. 

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
// 2. 
//builder.Services.AddControllersWithViews()
//    .AddViewLocalization
//    (LanguageViewLocationExpanderFormat.SubFolder)
//    .AddDataAnnotationsLocalization();
builder.Services.AddControllersWithViews()
    .AddViewLocalization
    (LanguageViewLocationExpanderFormat.SubFolder)
    .AddDataAnnotationsLocalization();
// 3. 
builder.Services.Configure<RequestLocalizationOptions>(options => {
    var supportedCultures = new[] { "en-US", "es" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});





//adding session
builder.Services.AddSession(options =>
{

    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    //  options.Cookie.SameSite=SameSiteMode.None;

});
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}



app.UseCors(X => X.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


// ------------

// app.UseHttpsRedirection();

// Simple dev proxy: forward requests that start with "/api" to the backend API
// so calls to https://localhost:7047/api/* reach the API running on localhost:5215.
var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator });
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api", out var remaining))
    {
        var targetBase = "http://localhost:5215"; // backend dev address
        var targetUri = new Uri(targetBase + context.Request.Path + context.Request.QueryString);

        using var requestMessage = new HttpRequestMessage(new HttpMethod(context.Request.Method), targetUri);

        // Copy request headers
        foreach (var header in context.Request.Headers)
        {
            if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, (IEnumerable<string>)header.Value))
            {
                requestMessage.Content ??= new StreamContent(Stream.Null);
                requestMessage.Content.Headers.TryAddWithoutValidation(header.Key, (IEnumerable<string>)header.Value);
            }
        }

        // Copy body if present
        if (context.Request.ContentLength > 0)
        {
            requestMessage.Content = new StreamContent(context.Request.Body);
        }

        var resp = await httpClient.SendAsync(requestMessage);

        context.Response.StatusCode = (int)resp.StatusCode;

        foreach (var header in resp.Headers)
            context.Response.Headers[header.Key] = header.Value.ToArray();
        if (resp.Content != null)
        {
            foreach (var header in resp.Content.Headers)
                context.Response.Headers[header.Key] = header.Value.ToArray();

            // Prevent duplicate content-length header handling by Kestrel
            context.Response.Headers.Remove("transfer-encoding");

            await resp.Content.CopyToAsync(context.Response.Body);
        }

        return;
    }

    await next();
});

// ------------
app.UseStaticFiles();
//app.UseExceptionHandlerMiddleware();

//4.
var supportedCultures = new[] { "en-US", "es" };
// 5. 
// Culture from the HttpRequest
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[1])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

//app.UseRequestLocalization(localizationOptions);
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


app.UseRouting();
//app.UseCookieAuthentication();
app.UseSession();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapControllerRoute(
    name: "default",
     pattern: "{controller=home}/{action=index}/{id?}");

app.Run();

