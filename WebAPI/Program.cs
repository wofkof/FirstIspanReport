
using WebAPI.Controllers;
using WebAPI.Repository.AttendanceRepository;
using WebAPI.Repository.NewFolder1;
using WebAPI.Repository.ProductsRepository;
using WebAPI.Repository.RevenueRepository;
using WebAPI.Services;
using 這是扭蛋機系統;

var builder = WebApplication.CreateBuilder(args);
var gashaponConnection = builder.Configuration.GetConnectionString("Gashapon");
// Add services to the container.

builder.Services.AddControllers();
if (!string.IsNullOrEmpty(gashaponConnection))
{
    builder.Services.AddSingleton<IDbService>(provider => new DbService(gashaponConnection));
    builder.Services.AddSingleton<IGashaponRepository, GashaponRepository>();
    builder.Services.AddSingleton<IPointsHistoryRepository, PointsHistoryRepository>();
    builder.Services.AddSingleton<IProductsRepository, ProductsRepository>();
    builder.Services.AddSingleton<ISubProductsRepository, SubProductsRepository>();
    builder.Services.AddSingleton<IAttendanceRepository, AttendanceRepository>();
    builder.Services.AddSingleton<IRevenueRepository, RevenueRepository>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
