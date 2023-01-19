using Mobiliva.Business.ProductManager;
using Mobiliva.DAL;
using Mobiliva.Entity.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mobiliva.Repository.Customers.Repository;
using Mobiliva.Repository.Products.Interface;
using Mobiliva.Business.Cache;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Mobiliva.Business.Extensions;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .Build();

#region Serilog

Logger log = new LoggerConfiguration()
                .MinimumLevel.Information()
                .ReadFrom.Configuration(config)
                .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(log);

#endregion


builder.Services.AddMemoryCache();

var mapperConfig = new MapperConfiguration(mc => {
    mc.AddProfile(new AutoMapperProfile());
});
IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton<ICacheManager, MemoryCacheManager>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();



builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationContext>(options => {

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

