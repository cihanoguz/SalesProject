using Microsoft.Extensions.Configuration;
using Mobiliva.RabbitMQ.Receiving.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mobiliva.RabbitMQ.Receiving.Services.Notifications;
using System.Text;
using Newtonsoft.Json;


IConfiguration configuration;

configuration = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .Build();

var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(configuration);
services.AddSingleton<RabbitmqListener>();
services.AddScoped<INotificationManager, NotificationManager>();

var serviceProvider = services.BuildServiceProvider();
var rabbit = serviceProvider.GetService<RabbitmqListener>();


rabbit.Run();









