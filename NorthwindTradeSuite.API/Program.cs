using NorthwindTradeSuite.Application.Extensions;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.DTOs;
using NorthwindTradeSuite.Infrastructure.Extensions;
using NorthwindTradeSuite.Mapping.AutoMapper;
using NorthwindTradeSuite.Persistence;
using NorthwindTradeSuite.Services.Extensions;
using System.Reflection;

var webApplicationBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.

webApplicationBuilder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
webApplicationBuilder.Services.AddEndpointsApiExplorer();

webApplicationBuilder.Services.AddSwaggerGen();

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

webApplication.UseAuthorization();

webApplication.MapControllers();

webApplication.Run();
