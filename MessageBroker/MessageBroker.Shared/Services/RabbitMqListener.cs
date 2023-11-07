using MessageBroker.Shared.Interfaces;
using RabbitMQ.Client;

namespace MessageBroker.Shared.Services
{
	public class RabbitMqReceiverService : IRabbitMqReceiverService
	{
		private readonly IConnection _connection;
		private readonly IModel _channel;
		private readonly DefaultBasicConsumer _consumer;

		public RabbitMqReceiverService(string routingKey, Func<IModel, DefaultBasicConsumer> createConsumer)
		{
			// use config file
			var factory = new ConnectionFactory { Uri = new Uri("") };
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.ExchangeDeclare(exchange: "catalog_update", type: ExchangeType.Direct);
			_channel.QueueDeclare(queue: "catalog_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
			_channel.QueueBind(queue: "catalog_queue", exchange: "catalog_update", routingKey: routingKey);
			_consumer = createConsumer(_channel);
		}

		public void ProcessMessage()
		{
			_channel.BasicConsume("catalog_queue", autoAck: false, _consumer);
		}

		public void Dispose()
		{
			_channel.Close();
			_connection.Close();
		}
	}
}
