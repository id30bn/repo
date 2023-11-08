using MessageBroker.Shared;
using MessageBroker.Shared.Interfaces;

namespace Carting.Services.MessageBroker
{
	public class CatalogListenerService : BackgroundService
	{
		private readonly IRabbitMqReceiverService _receiverService;

		public CatalogListenerService(IRabbitMqFactory rabbitMqFactory)
		{
			_receiverService = rabbitMqFactory.CreateReceiver(RoutingKey.Item);
		}

		protected override Task ExecuteAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			_receiverService.ConsumeMessage();

			return Task.CompletedTask;
		}

		public override void Dispose()
		{
			_receiverService.Dispose();
			base.Dispose();
		}
	}
}
