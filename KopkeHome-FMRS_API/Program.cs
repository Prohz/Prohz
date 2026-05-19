// global using KopkeHome_BusinessLayer.Interface;
// global using KopkeHome_BusinessLayer.Services;
// using KopkeHome_DataAccessLayer;
// using KopkeHome_DataAccessLayer.GenericRepository;
// using KopkeHome_DataAccessLayer.Repository;
// using KopkeHome_ModelLayer.DataModel;
// using KopkeHome_UtilityLayer;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;
// using Microsoft.OpenApi.Models;
// using Serilog;
// using Swashbuckle.AspNetCore.Filters;
// using System.Text;
// using Microsoft.Extensions.Logging;
// using System.Net.Security;
// using System.Net;
// using System.Security.Cryptography.X509Certificates;

// var builder = WebApplication.CreateBuilder(args);

// // Ignore SSL errors
// ServicePointManager.ServerCertificateValidationCallback =
// delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
// {
//     return true; // Always accept
// };
// //IApplicationBuilder configuration = app.Configure;


// // using serilog.
// var logger = new LoggerConfiguration()
//   .ReadFrom.Configuration(builder.Configuration)
//   .Enrich.FromLogContext()
//   .CreateLogger();
// builder.Logging.ClearProviders();
// builder.Logging.AddSerilog(logger);
// builder.Logging.AddConsole();
// builder.Logging.AddDebug();
// builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Debug);
// builder.Logging.AddFilter("System", LogLevel.Debug);
// builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information);


// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//   {
//       options.SaveToken = true;
//       options.RequireHttpsMetadata = false;
//       options.TokenValidationParameters = new TokenValidationParameters
//       {
//           ValidateIssuer = true,
//           ValidateAudience = true,
//           ValidateIssuerSigningKey = true,
//           ValidIssuer = builder.Configuration["Jwt:Issuer"],
//           ValidAudience = builder.Configuration["Jwt:Audience"],
//           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//       };
//   });
// builder.Services.AddCors();
// builder.Services.AddControllers();

// builder.Services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("KopkeHome_WebContext"));
//     options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
// }) ;

// var conn = builder.Configuration.GetConnectionString("KopkeHome_WebContext");
// Console.WriteLine("DB CONNECTION: " + conn);

// builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// builder.Services.Configure<CookiePolicyOptions>(options =>
// {

//     options.Secure = CookieSecurePolicy.SameAsRequest;

//     // refer "SameSite cookies" on mozilla website 
//     options.MinimumSameSitePolicy = SameSiteMode.None;

// });

// builder.Services.AddHttpContextAccessor();
// builder.Services.AddScoped<IBusinessProfile, BusinessProfileService>();
// // builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// builder.Services.AddAutoMapper(typeof(Program));

// builder.Services.AddScoped(typeof(DapperRepository<>));
// builder.Services.AddScoped<DapperContext>();
// builder.Services.AddScoped<IEmailService, EmailService>();
// builder.Services.AddScoped<IAccount, AccountService>();
// builder.Services.AddScoped<IDashboard, DashboardService>();
// builder.Services.AddScoped<IMembership, MembershipService>();
// builder.Services.AddScoped<IAdmin, AdminService>();
// builder.Services.AddScoped<IPaymentService, PaymentService>();
// builder.Services.AddScoped<IConfigFactory, ConfigFactory>();
// builder.Services.AddScoped<IGenericRepository, GenericRepository>();

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(options =>
// {
//     options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//     {
//         Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
//         In = ParameterLocation.Header,
//         Name = "Authorization",
//         Type = SecuritySchemeType.ApiKey,
//         BearerFormat = "JWT",
//         Scheme = "bearer"
//     });

//     options.AddSecurityRequirement(new OpenApiSecurityRequirement
//         {
//             {
//                 new OpenApiSecurityScheme
//                 {
//                     Reference = new OpenApiReference
//                     {
//                         Type=ReferenceType.SecurityScheme,
//                         Id="Bearer"
//                     }
//                 },
//                 new string[]{}
//             }
//         });

//     options.OperationFilter<SecurityRequirementsOperationFilter>();
// });

// builder.Services.AddIdentity<User, Role>(options =>
// {
//     options.Password.RequireDigit = false;
//     options.Password.RequiredLength = 8;
//     options.Password.RequireNonAlphanumeric = false;
//     options.Password.RequireUppercase = false;
//     options.Password.RequireLowercase = false;
// }).AddEntityFrameworkStores<ApplicationDbContext>()
//     .AddDefaultTokenProviders();



// // --------------------

// builder.Services.ConfigureApplicationCookie(options =>
// {
//     options.Events.OnRedirectToLogin = context =>
//     {
//         context.Response.StatusCode = 401;
//         return Task.CompletedTask;
//     };

//     options.Events.OnRedirectToAccessDenied = context =>
//     {
//         context.Response.StatusCode = 403;
//         return Task.CompletedTask;
//     };
// });

// // --------------------



// var app = builder.Build();

// // Apply any pending EF Core migrations and ensure database is created when running locally
// using (var scope = app.Services.CreateScope())
// {
//     var dbLogger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

//     try
//     {
//         var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//         dbLogger.LogInformation("Checking database connection...");

//         bool canConnect = db.Database.CanConnect();

//         if (canConnect)
//         {
//             dbLogger.LogInformation("Database connection SUCCESSFUL.");
//         }
//         else
//         {
//             dbLogger.LogWarning("Database connection FAILED.");
//         }

//         dbLogger.LogInformation("Applying migrations...");
//         db.Database.Migrate();

//         dbLogger.LogInformation("Database migration completed successfully.");
//     }
//     catch (Exception ex)
//     {
//         dbLogger.LogError(ex, "Database initialization FAILED.");
//     }
// }

// if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
// {

//     app.UseSwagger();
//     app.UseSwaggerUI(c =>
//     {
//         string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
//         c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Web API");

//     });
// }

// // -------------------------------

// // var env = app.Environment.EnvironmentName;
// // Console.WriteLine("Environment: " + env);

// // var conn = builder.Configuration.GetConnectionString("KopkeHome_WebContext");
// // Console.WriteLine("Connection: " + conn);

// // ----------------------------------

// app.UseCors(X => X.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// // app.UseHttpsRedirection();


// app.UseAuthentication();
// app.UseAuthorization();

// // app.UseCookiePolicy();


// app.MapControllers();
// app.Run();








global using KopkeHome_BusinessLayer.Interface;
global using KopkeHome_BusinessLayer.Services;
using KopkeHome_DataAccessLayer;
using KopkeHome_DataAccessLayer.GenericRepository;
using KopkeHome_DataAccessLayer.Repository;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_UtilityLayer;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

#region Ignore SSL (DEV ONLY)
ServicePointManager.ServerCertificateValidationCallback =
delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
{
    return true;
};
#endregion

#region Logging (Serilog)
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Logging.AddConsole();
builder.Logging.AddDebug();
#endregion

#region Services

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("KopkeHome_WebContext"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(DapperRepository<>));
builder.Services.AddScoped<DapperContext>();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAccount, AccountService>();
builder.Services.AddScoped<IDashboard, DashboardService>();
builder.Services.AddScoped<IMembership, MembershipService>();
builder.Services.AddScoped<IAdmin, AdminService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IConfigFactory, ConfigFactory>();
builder.Services.AddScoped<IGenericRepository, GenericRepository>();
builder.Services.AddScoped<IBusinessProfile, BusinessProfileService>();

#endregion

#region Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        )
    };
});

#endregion

#region Identity

builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});

#endregion

#region Swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Bearer {token}",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

#endregion

var app = builder.Build();

#region DB Migration
using (var scope = app.Services.CreateScope())
{
    var dbLogger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbLogger.LogInformation("Checking database connection...");
        db.Database.CanConnect();

        dbLogger.LogInformation("Applying migrations...");
        db.Database.Migrate();

        dbLogger.LogInformation("Database ready.");
    }
    catch (Exception ex)
    {
        dbLogger.LogError(ex, "Database initialization FAILED.");
    }
}
#endregion

#region Middleware Pipeline

// IMPORTANT: must be FIRST if using reverse proxy (/api)
app.UseForwardedHeaders();


// app.UsePathBase("/api");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Web API");
    
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API");
    c.RoutePrefix = "swagger";
});

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();