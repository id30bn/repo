using MessageBroker.Shared;
using MessageBroker.Shared.Interfaces;
using MessageBroker.Shared.Services;
using Microsoft.Extensions.Options;

namespace Carting.Services.MessageBroker
{
	public class RabbitMqFactory : IRabbitMqFactory
	{
		private readonly ICartingService _cartingService;
		private readonly IOptions<RabbitMqOptions> _options;

		public RabbitMqFactory(ICartingService cartingService, IOptions<RabbitMqOptions> options)
		{
			_cartingService = cartingService;
			_options = options;
		}

		public IRabbitMqReceiverService CreateReceiver(string routingKey)
		{
			switch (routingKey) {
				case RoutingKey.Item:
					return new RabbitMqReceiverService(routingKey, channel => new ItemMessageHandler(channel, _cartingService), _options);
				default:
					throw new ArgumentException(nameof(routingKey));
			}
		}
	}
}
