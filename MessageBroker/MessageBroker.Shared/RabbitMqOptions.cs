namespace MessageBroker.Shared
{
	public class RabbitMqOptions
	{
		public string ConnectionString { get; set; }

		public string QueueName { get; set; }

		public string ExchangeName { get; set; }

		public int RetryCount { get; set; }
	}
}
