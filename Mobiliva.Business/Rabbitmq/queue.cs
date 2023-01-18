using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
namespace Mobiliva.API.Services.Rabbitmq
{
	public class queue
	{
        #region 
        private IConnection connection;
		private IModel _channel;
		private IModel channel => _channel ?? (_channel = CreateOrGetChannel()); 
		private bool isConnectionOpen;


        #endregion

        public void Connect()
		{
			if (!isConnectionOpen || connection == null)
			{
				connection = GetConnection();
			}
			else
			{
				connection.Close();
			}


			isConnectionOpen = connection.IsOpen;
		}

        public void DeclareExchange()
        {
			channel.ExchangeDeclare("testexchange","direct");
        }

		public void DeclareQueue()
		{
			channel.QueueDeclare("testqueue");
		}

		public void BindQueue()
		{
			channel.QueueBind("testqueue", "testexchange", "testqueue");
		}

		public void Publish()
		{
			WriteDataToExchange("testexchange", "testqueue", "asda");
		}

		public void TestRabbitmq()
		{
            var factory = new ConnectionFactory() { Uri = new Uri("yourdata", UriKind.RelativeOrAbsolute) };

            using (var connection = factory.CreateConnection())
            {
				//Mesajı iletmek için kanal oluşutuyoruz.
				try
				{
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare("MyFirstQueue",
                                    false, //Fiziksel mi saklanacak memory üzerinde mi ?
                                    false, //Farklı bağlantılar ile kullanım
                                    false, //Consumer lar kullandıktan sonra otomatik silinmesi
                                    arguments: null); //Exchange tipleri

						string mail = "cihanoguz92@gmail.com";

                        string message = JsonConvert.SerializeObject(mail);
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                            routingKey: "MyFirstQueue",
                            basicProperties: null,
                            body: body);

                        //      Console.WriteLine(" [x] Sent {0}", message);
                    }
                }
				catch (Exception ex)
				{

				}

               // Console.WriteLine(" Press [enter] to exit.");
               // Console.ReadLine();

            }
        }

        #region helper
        private IModel CreateOrGetChannel()
		{
			return connection.CreateModel();
        }

        private IConnection GetConnection()
		{
            ConnectionFactory factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://cihanoguz92@gmail.com:Yorumyok2*\"@3.73.145.125:5672", UriKind.RelativeOrAbsolute)
            };

			return factory.CreateConnection();
        }

		private void WriteDataToExchange(string exchangName, string routingKey, object data)
		{
			var dataArr = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));


			channel.BasicPublish(exchangName, routingKey, null, dataArr);
		}

        #endregion

    }
}

