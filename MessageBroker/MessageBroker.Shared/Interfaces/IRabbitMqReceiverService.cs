namespace MessageBroker.Shared.Interfaces
{
	public interface IRabbitMqReceiverService : IDisposable
	{
		void ConsumeMessage();
	}
}
