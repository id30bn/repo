using MessageBroker.Shared;
using MessageBroker.Shared.Interfaces;
using MessageBroker.Shared.Services;

namespace Carting.Services.MessageBroker
{
	public class RabbitMqFactory : IRabbitMqFactory
	{
		private readonly ICartingService _cartingService;
		public RabbitMqFactory(ICartingService cartingService)
		{
			_cartingService = cartingService;
		}

		public IRabbitMqReceiverService CreateListener(string routingKey)
		{
			switch (routingKey) {
				case RoutingKey.Item:
					return new RabbitMqReceiverService(routingKey, channel => new ItemMessageConsumer(channel, _cartingService));
				default:
					throw new ArgumentException(nameof(routingKey));
			}
		}
	}
}
