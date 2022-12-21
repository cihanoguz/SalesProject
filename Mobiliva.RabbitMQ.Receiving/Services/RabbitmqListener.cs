using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mobiliva.RabbitMQ.Receiving.Services.Notifications;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Mobiliva.RabbitMQ.Receiving.Services
{
	public class RabbitmqListener
    {
        private readonly IConfiguration _configuration;
        private readonly INotificationManager _notificationManager;
        public RabbitmqListener(IConfiguration configuration , INotificationManager notificationManager)
        {
            _configuration = configuration;
            _notificationManager = notificationManager;
        }

        public void Run()
		{
            /*
             * NotificationManager
             * Mail atma işlemini business içerisinde halletmek lazım. done 
             * Sms atma işlemi business içerisinde halletmek lazım. done 
             * Backoffice gibi bir arayüz olup buradan gönderilecek mailin içerği db'ye basılıp dinamik olarak kod tarafında ilgili mail sablonu oluşturulabilir.
             * MessageQueqe Rabbit kullanyoruz. İleride yeni bir queq yapısı kullansak bunu dönüştürebilir miyiz ? Kafka kullanacağım sistemi değiştir.
             * Memory Cache kullanıyor ama muadil olarak dedi ki Redis kullanalaım sistemi çevirebilmek için modüler gitmek lazım
             * memory cache midddleware olarak yapılabilir mi 
             * page işlemleri için de bir method yap parametreye bağlı geri dönsün
             * 1.Entity var ,2.Db'ye göndermek için bir Record olacak. 3. Response View modellerin oalcak.
             * Örnerğin, Order db entity,2.OrderRequestModel (record, Business'a gidecek model), 3.OrderResponseViewModel
             * Test
             */
            /*var services = new ServiceCollection();
            var serviceProvider = services.BuildServiceProvider();

            var _mailManager = serviceProvider.GetService<MailManager.MailManager>();*/

            var factory = new ConnectionFactory() { Uri = new Uri(_configuration["ConnectionStrings:RabbitMqString"], UriKind.RelativeOrAbsolute) };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //Queue Declaration
                    channel.QueueDeclare(queue: "MyFirstQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);


                    var consumer = new EventingBasicConsumer(channel);
                    //This is for contionus listening
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body,1,body.Length-2);
                        Console.WriteLine(message);
                        _notificationManager.SendMail("reset your password", message/*, null, null, null*/);
                    };
                    //pulling messages
                    //autoAck : true 
                    channel.BasicConsume(queue: "MyFirstQueue",
                        autoAck: true,
                        consumer: consumer);
                    Console.ReadLine();

                }
            }
        }
  }
}

