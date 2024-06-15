using NorthwindTradeSuite.Infrastructure.Extensions;
using NorthwindTradeSuite.Persistence;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Persistence.Repositories.Implementation;

var webApplicationBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.

webApplicationBuilder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
webApplicationBuilder.Services.AddEndpointsApiExplorer();

webApplicationBuilder.Services.AddSwaggerGen();

webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>();

webApplicationBuilder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
webApplicationBuilder.Services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(DeletableEntityRepository<>));

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
