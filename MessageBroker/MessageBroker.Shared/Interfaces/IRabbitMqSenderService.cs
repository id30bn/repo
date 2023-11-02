namespace MessageBroker.Shared.Interfaces
{
	public interface IRabbitMqSenderService
	{
		void SendMessage(object obj);

		void SendMessage(string message);
	}
}
