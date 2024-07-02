using AutoMapper;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.DTOs;
using NorthwindTradeSuite.Infrastructure.Extensions;
using NorthwindTradeSuite.Mapping.AutoMapper;
using NorthwindTradeSuite.Persistence;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Persistence.Repositories.Implementation;
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
    Assembly.GetAssembly(typeof(BaseEntity<string>))!,
    Assembly.GetAssembly(typeof(DTO))!
};

AutoMapperConfigurator.RegisterMappings(assemblies.ToArray());

webApplicationBuilder.Services.AddSingleton<IMapper>(AutoMapperConfigurator.MapperInstance);

webApplicationBuilder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
webApplicationBuilder.Services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(DeletableEntityRepository<>));

webApplicationBuilder.Services.AddServiceLayer();

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
