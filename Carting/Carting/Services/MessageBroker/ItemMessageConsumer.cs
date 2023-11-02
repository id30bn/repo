using Carting.Core.Models.Cart;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Carting.Services.MessageBroker
{
	public class ItemMessageConsumer : DefaultBasicConsumer
	{
		private readonly IModel _channel;
		private readonly ICartingService _cartingService;

		public ItemMessageConsumer(IModel channel, ICartingService cartingService)
		{
			_channel = channel;
			_cartingService = cartingService;
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
			var item = JsonSerializer.Deserialize<Item>(content);
			_channel.BasicAck(deliveryTag, multiple: false);
		}
	}
}
