using Carting.Core.Models.Cart;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Carting.Services.MessageBroker
{
	public class ItemMessageHandler : MessageHandler
	{
		private readonly ICartingService _cartingService;
		private JsonSerializerSettings _jsonSettings;

		public ItemMessageHandler(IModel channel, ICartingService cartingService)
			: base(channel)
		{
			_cartingService = cartingService;
			_jsonSettings = new JsonSerializerSettings() {
				Converters = new List<JsonConverter> { new ItemJsonConverter() }
			};
		}

		public override void ProcessMessage(string message)
		{
			var receivedItem = JsonConvert.DeserializeObject<Item>(message, _jsonSettings);
			_cartingService.UpdateItem(receivedItem);
		}
	}
}
