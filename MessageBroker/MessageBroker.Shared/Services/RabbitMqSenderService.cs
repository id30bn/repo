using MessageBroker.Shared.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MessageBroker.Shared.Services
{
	public class RabbitMqSenderService : IRabbitMqSenderService
	{
		public void SendMessage(object obj)
		{
			var serializeOptions = new JsonSerializerOptions {
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};
			var message = JsonSerializer.Serialize(obj, serializeOptions);
			SendMessage(message);
		}

		public void SendMessage(string message)
		{
			// use config file
			var factory = new ConnectionFactory() { Uri = new Uri("amqps://qimrjxng:BPiepPZNRJRa2R48Yu2TfK4wpsEhYBzG@cow.rmq2.cloudamqp.com/qimrjxng") };
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel()) {

				channel.ExchangeDeclare(exchange: "catalog_update", type: ExchangeType.Direct);

				var body = Encoding.UTF8.GetBytes(message);

				var properties = channel.CreateBasicProperties();
				properties.Persistent = true;

				channel.BasicPublish(exchange: "catalog_update",
					 routingKey: RoutingKey.Item,
					 basicProperties: properties,
					 body: body);
			}
		}
	}
}
