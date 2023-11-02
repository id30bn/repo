using MessageBroker.Shared;
using MessageBroker.Shared.Interfaces;

namespace Carting.Services.MessageBroker
{
	public class CatalogListenerService : BackgroundService
	{
		private readonly IRabbitMqReceiverService _listenerService;

		public CatalogListenerService(IRabbitMqFactory rabbitMqFactory)
		{
			_listenerService = rabbitMqFactory.CreateListener(RoutingKey.Item);
		}

		protected override Task ExecuteAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			_listenerService.ProcessMessage();

			return Task.CompletedTask;
		}

		public override void Dispose()
		{
			_listenerService.Dispose();
			base.Dispose();
		}
	}
}
