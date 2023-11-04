using Carting.Core.Models.Cart;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Carting.Services.MessageBroker
{
	public class ItemMessageConsumer : DefaultBasicConsumer
	{
		private readonly IModel _channel;
		private readonly ICartingService _cartingService;
		private JsonSerializerSettings _jsonSettings;

		public ItemMessageConsumer(IModel channel, ICartingService cartingService)
		{
			_channel = channel;
			_cartingService = cartingService;
			_jsonSettings = new JsonSerializerSettings() {
				Converters = new List<JsonConverter> { new ItemJsonConverter() }
			};
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
			var receivedItem = JsonConvert.DeserializeObject<Item>(content, _jsonSettings);
			_cartingService.UpdateItem(receivedItem);

			_channel.BasicAck(deliveryTag, multiple: false);
		}
	}
}
