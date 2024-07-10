using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NorthwindTradeSuite.API.Middlewares;
using NorthwindTradeSuite.Application.Extensions;
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
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationUserConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationRoleConstants;
using NorthwindTradeSuite.Domain;

var webApplicationBuilder = WebApplication.CreateBuilder(args);

webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>();

Assembly[] assemblies = new Assembly[]
{
    Assembly.GetAssembly(typeof(DomainAssemblyMarker))!,
    Assembly.GetAssembly(typeof(DTOsAssemblyMarker))!
};

AutoMapperConfigurator.RegisterMappings(assemblies.ToArray());

webApplicationBuilder.Services.AddSingleton(AutoMapperConfigurator.MapperInstance);

webApplicationBuilder.Services.AddRepositories();

webApplicationBuilder.Services.AddServices();

webApplicationBuilder.Services.AddApplicationLayer();

webApplicationBuilder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(identityOptions =>
    {
        identityOptions.SignIn.RequireConfirmedAccount = false;
        identityOptions.SignIn.RequireConfirmedEmail = false;
        identityOptions.SignIn.RequireConfirmedPhoneNumber = false;
        identityOptions.User.AllowedUserNameCharacters = ALLOWED_USERNAME_CHARACTERS;
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
    corsOptions.AddPolicy("AllowAll", corsPolicyBuilder =>
        corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

webApplicationBuilder.Services
    .AddAuthentication(authenticationOptions =>
    {
        authenticationOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
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
            ClockSkew = TimeSpan.FromMinutes(5)
        };

        jwtBearerOptions.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = authenticationFailedContext =>
            {
                authenticationFailedContext.Response.OnStarting(() =>
                {
                    var logger = authenticationFailedContext.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogError($"Authentication failed: {authenticationFailedContext.Exception.Message}");
                    return Task.CompletedTask;
                });
                
                return Task.CompletedTask;
            },
            OnTokenValidated = tokenValidatedContext =>
            {
                var logger = tokenValidatedContext.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogInformation($"Token validated for user: {tokenValidatedContext.Principal!.Identity!.Name}");
                return Task.CompletedTask;
            },
            OnChallenge = jwtBearerChallengeContext =>
            {
                jwtBearerChallengeContext.Response.OnStarting(() =>
                {
                    var logger = jwtBearerChallengeContext.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogError($"OnChallenge error: {jwtBearerChallengeContext.ErrorDescription}");
                    return Task.CompletedTask;
                });

                return Task.CompletedTask;
            }
        };
    });

webApplicationBuilder.Services
    .AddAuthorization(authorizationOptions =>
    {
        authorizationOptions.AddPolicy(USER_POLICY, authorizationPolicyBuilder =>
        {
            authorizationPolicyBuilder.RequireRole(NORMAL_USER, ADMINISTRATOR);
        });

        authorizationOptions.AddPolicy(ADMINISTRATOR_POLICY, authorizationPolicyBuilder =>
        {
            authorizationPolicyBuilder.RequireRole(ADMINISTRATOR);
        });
    });

webApplicationBuilder.Services
    .AddSwaggerGen(swaggerGenOptions =>
    {
        swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Northwind Trade Suite API",
            Version = "v1",
        });

        swaggerGenOptions.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter the JWT token with format: Bearer[space] token"
        });

        swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
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

webApplication.UseRouting();

webApplication.UseCors("AllowAll");

webApplication.UseAuthentication();

webApplication.UseAuthorization();

webApplication.UseMiddleware<LoggingMiddleware>();

webApplication.UseMiddleware<GlobalExceptionHandlingMiddleware>();

webApplication.MapControllers();

webApplication.Run();
