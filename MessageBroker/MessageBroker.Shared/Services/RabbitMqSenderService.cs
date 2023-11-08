using MessageBroker.Shared.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MessageBroker.Shared.Services
{
	public class RabbitMqSenderService : IRabbitMqSenderService
	{
		private string _connectionString;
		private string _exchangeName;

		public RabbitMqSenderService(string connectionString, string exchangeName)
		{
			_connectionString = connectionString;
			_exchangeName = exchangeName;

		}

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
			var factory = new ConnectionFactory() { Uri = new Uri(_connectionString) };
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel()) {

				channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);

				var body = Encoding.UTF8.GetBytes(message);

				var properties = channel.CreateBasicProperties();
				properties.Persistent = true;

				channel.BasicPublish(exchange: _exchangeName,
					 routingKey: RoutingKey.Item,
					 basicProperties: properties,
					 body: body);
			}
		}
	}
}
