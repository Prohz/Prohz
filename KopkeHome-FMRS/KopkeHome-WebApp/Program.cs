using KopkeHome_UtilityLayer;
using KopkeHome_UtilityLayer.ExceptionHandler;
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

