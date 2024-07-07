using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NorthwindTradeSuite.API.Middlewares;
using NorthwindTradeSuite.Application.Extensions;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Domain.Entities.Identity;
using NorthwindTradeSuite.DTOs;
using NorthwindTradeSuite.Infrastructure.Extensions;
using NorthwindTradeSuite.Mapping.AutoMapper;
using NorthwindTradeSuite.Persistence;
using NorthwindTradeSuite.Persistence.Extensions;
using NorthwindTradeSuite.Services.Extensions;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationRoleConstants;

var webApplicationBuilder = WebApplication.CreateBuilder(args);

webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>();

Assembly[] assemblies = new Assembly[]
{
    Assembly.GetAssembly(typeof(EntityMarker))!,
    Assembly.GetAssembly(typeof(DTOMarker))!
};

AutoMapperConfigurator.RegisterMappings(assemblies.ToArray());

webApplicationBuilder.Services.AddSingleton(AutoMapperConfigurator.MapperInstance);

webApplicationBuilder.Services.AddPersistenceLayerServices();

webApplicationBuilder.Services.AddDatabaseServices();

webApplicationBuilder.Services.AddApplicationLayer();

webApplicationBuilder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(identityOptions =>
    {
        identityOptions.SignIn.RequireConfirmedAccount = false;
        identityOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

webApplicationBuilder.Services
    .AddControllers()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

webApplicationBuilder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("AllowedOrigins",
             p => p.WithOrigins(webApplicationBuilder.Configuration["CorsPolicyOptions:Hosts"])
                   .AllowAnyMethod()
                   .AllowAnyHeader());
});

webApplicationBuilder.Services
    .AddAuthentication(authenticationOptions =>
    {
        authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwtBearerOptions =>
    {
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = webApplicationBuilder.Configuration["JWT:Issuer"],
            ValidAudience = webApplicationBuilder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(webApplicationBuilder.Configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero,
        };
    }
);

webApplicationBuilder.Services
    .AddAuthorization(authorizationOptions =>
    {
        authorizationOptions.AddPolicy("UserPolicy", 
            authorizationPolicyBuilder => authorizationPolicyBuilder.RequireRole(NORMAL_USER, ADMINISTRATOR));
        authorizationOptions.AddPolicy("AdminPolicy", 
            authorizationPolicyBuilder => authorizationPolicyBuilder.RequireRole(ADMINISTRATOR));
    });

webApplicationBuilder.Services
    .AddSwaggerGen(swaggerGenOptions =>
    {
        swaggerGenOptions.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    });

webApplicationBuilder.Services.AddEndpointsApiExplorer();

webApplicationBuilder.Services
    .Configure<CookieAuthenticationOptions>(cookieAuthenticationOptions =>
    {
        cookieAuthenticationOptions.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

var webApplication = webApplicationBuilder.Build();

var logger = webApplication.Services.GetService<ILogger<Program>>()!;

if (webApplication.Environment.IsDevelopment())
{
    webApplication.MigrateDatabaseAsync(logger).GetAwaiter().GetResult();

    webApplication.SeedDatabaseAsync(logger).GetAwaiter().GetResult();

    webApplication.UseSwagger();

    webApplication.UseSwaggerUI();
}

webApplication.UseHttpsRedirection();

webApplication.UseAuthentication();

webApplication.UseAuthorization();

webApplication.UseMiddleware<LoggingMiddleware>();

webApplication.UseMiddleware<GlobalExceptionHandlingMiddleware>();

webApplication.MapControllers();

webApplication.Run();
