using DMello.Application.Auth;
using DMello.Application.Common.Interfaces;
using DMello.Application.Common.Options;
using DMello.Domain.Interfaces;
using DMello.Infrastructure.Authentication;
using DMello.Infrastructure.Data;
using DMello.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

#region All Regisetered Service via DI
// 2. Registering ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IJwtService, JwtService>();

// 2. UserRepository (Data access layer)
builder.Services.AddScoped<IUserRepository, UserRepository>();

// 3. AuthService (Business logic layer)
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddLogging();
#endregion

// Bind appsettings.json "Jwt" section directly to JwtOptions class
builder.Services.Configure<JwtOptions>
    (builder.Configuration.GetSection(JwtOptions.SectionName));



#region // Added necessary JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero, // For Expiring Access Token in 10 secs, it is temporary and must be only used for testing
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "YourSuperSecretKey1234567890123456")
        )
    };
});

#endregion


#region// 1. Configure Logging to stdout for Azure App Service
//Why it exists: Sends internal app logs, EF Core SQL queries, and unhandled exceptions to the standard output console.
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Outputs errors directly to Azure Log Stream
builder.Logging.AddDebug();
#endregion

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://green-cliff-08809cc1e.2.azurestaticapps.net")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // <--- CRITICAL: Allows browser to send HttpOnly cookies
    });
});
#endregion



var app = builder.Build();


#region Configure Http request pipeline
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwaggerUI(options =>
    {
        // Points Swagger UI to the Native .NET OpenAPI JSON endpoint
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.RoutePrefix = "swagger"; // Opens UI at /swagger
    });
}
#endregion

#region // Run migrations and seeding inside a guarded try-catch block

//Why it exists: Isolates database startup operations (migrations/seeding) inside a scoped try-catch block.
//What problem it solves: Prevents a single database schema constraint violation from causing a total HTTP 500 App Service crash.
//How it works: Catches DbUpdateException or SqlException during app startup, logs the precise stack trace via ILogger, and lets the Web API process continue serving requests.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // 1. Apply pending EF Core migrations safely
        logger.LogInformation("Applying EF Core migrations...");
        context.Database.Migrate();

        // 2. Safe seed check for testing user
        var user = context.Users.FirstOrDefault(u => u.Email == "testing@gmail.com");
        if (user != null)
        {
            logger.LogInformation("Updating password hash for testing@gmail.com...");
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234");
            context.SaveChanges();
            logger.LogInformation("Password hash updated successfully!");
        }
    }
    catch (Exception ex)
    {
        // Log the exact database error without killing the API process
        logger.LogError(ex, "DATABASE STARTUP ERROR: Migration or seeding failed.");
    }
}
#endregion
app.UseCors("AllowAngular");
app.UseRouting();
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

#region // Automatically apply pending EF Core migrations on application startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>(); // Replace with your DbContext class name
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database schema.");
    }
}
#endregion

app.Run();
