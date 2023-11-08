using RabbitMQ.Client;
using System.Text;

namespace Carting.Services.MessageBroker
{
	public abstract class MessageHandler : DefaultBasicConsumer
	{
		private readonly IModel _channel;

		public abstract void ProcessMessage(string message);

		public MessageHandler(IModel channel)
		{
			_channel = channel;
		}

		public override void HandleBasicDeliver(string consumerTag,
			ulong deliveryTag,
			bool redelivered,
			string exchange,
			string routingKey,
			IBasicProperties properties,
			ReadOnlyMemory<byte> body)
		{
			var content = Encoding.UTF8.GetString(body.ToArray());

			ProcessMessage(content);

			_channel.BasicAck(deliveryTag, multiple: false);
		}
	}
}
