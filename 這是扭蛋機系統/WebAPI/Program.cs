using WebAPI.Repository.NewFolder1;
using WebAPI.Repository.ProductsRepository;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var gashaponConnection = builder.Configuration.GetConnectionString("Gashapon");
// Add services to the container.

builder.Services.AddControllers();
if (!string.IsNullOrEmpty(gashaponConnection))
{
    builder.Services.AddSingleton<IDbService>(provider => new DbService(gashaponConnection));
    builder.Services.AddSingleton<IGashaponRepository, GashaponRepository>();
    builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
    builder.Services.AddSingleton<IOrderDetailRepository, OrderDetailRepository>();
    builder.Services.AddSingleton<IPointsHistoryRepository, PointsHistoryRepository>();
    builder.Services.AddSingleton<IOrderPointsHistoryRepository, OrderPointsHistoryRepository>();
    builder.Services.AddSingleton<IProductsRepository, ProductsRepository>();
    builder.Services.AddSingleton<ISubProductsRepository, SubProductsRepository>();
}


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
