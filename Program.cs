using NLog.Web;
using NLog;
using GymApi;
using GymApi.Entities;
using GymApi.Services;
using GymApi.Middleware;
using GymApi.Authentication;
using FluentValidation;
using GymApi.Models;
using GymApi.Models.Validators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        var builder = WebApplication.CreateBuilder(args);
        var authenticationSettings = new AuthenticationSettings();

        builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = "Bearer";
            option.DefaultScheme = "Bearer";
            option.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
            };
        });

        //Nlog
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();
        // Add services to the container.
        builder.Services.AddScoped<GymSeeder>();
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        builder.Services.AddDbContext<GymDbContext>();
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var db = new GymDbContext();
        var app = builder.Build();
        var seeder = new GymSeeder(db);
        seeder.Seed(app);

        // Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseAuthorization(); 

        app.MapControllers();

        app.Run();
    }
}