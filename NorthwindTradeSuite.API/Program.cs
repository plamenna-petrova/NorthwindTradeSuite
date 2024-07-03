using NorthwindTradeSuite.API.Middlewares.Contracts;
using NorthwindTradeSuite.API.Middlewares;
using NorthwindTradeSuite.Application.Extensions;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.DTOs;
using NorthwindTradeSuite.Infrastructure.Extensions;
using NorthwindTradeSuite.Mapping.AutoMapper;
using NorthwindTradeSuite.Persistence;
using NorthwindTradeSuite.Services.Extensions;
using System.Reflection;
using System.Text.Json.Serialization;

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

webApplicationBuilder.Services.AddSingleton<IExceptionHandler, FluentValidationExceptionHandler>();

webApplicationBuilder.Services
    .AddControllers()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

webApplicationBuilder.Services.AddSwaggerGen();

webApplicationBuilder.Services.AddEndpointsApiExplorer();

var webApplication = webApplicationBuilder.Build();

var logger = webApplication.Services.GetService<ILogger<Program>>()!;

// Configure the HTTP request pipeline.
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
