using MessageBroker.Shared.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MessageBroker.Shared.Services
{
	public class RabbitMqReceiverService : IRabbitMqReceiverService
	{
		private readonly IConnection _connection;
		private readonly IModel _channel;
		private readonly DefaultBasicConsumer _messageHandler;

		private string _queueName;

		public RabbitMqReceiverService(string routingKey,
			Func<IModel, DefaultBasicConsumer> createHandler,
			IOptions<RabbitMqOptions> options)
		{
			var factory = new ConnectionFactory { Uri = new Uri(options.Value.ConnectionString) };
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_queueName = options.Value.QueueName;

			_channel.ExchangeDeclare(exchange: options.Value.ExchangeName, type: ExchangeType.Direct);
			_channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
			_channel.QueueBind(queue: _queueName, exchange: options.Value.ExchangeName, routingKey: routingKey);
			_messageHandler = createHandler(_channel);
		}

		public void ConsumeMessage()
		{
			_channel.BasicConsume(_queueName, autoAck: false, _messageHandler);
		}

		public void Dispose()
		{
			_channel.Close();
			_connection.Close();
		}
	}
}
