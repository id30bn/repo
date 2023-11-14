using RabbitMQ.Client;
using System.Text;

namespace Carting.Services.MessageBroker
{
	public abstract class MessageHandler : DefaultBasicConsumer
	{
		public const int MaxRetryCount = 3;

		private readonly IModel _channel;
		private readonly int _retryCount;
		private readonly TimeSpan _delayInSeconds = TimeSpan.FromSeconds(3);

		public MessageHandler(IModel channel, int retryCount)
		{
			_channel = channel;
			_retryCount = (retryCount > MaxRetryCount) ? MaxRetryCount : retryCount;
		}

		public abstract void ProcessMessage(string message);

		public override void HandleBasicDeliver(string consumerTag,
			ulong deliveryTag,
			bool redelivered,
			string exchange,
			string routingKey,
			IBasicProperties properties,
			ReadOnlyMemory<byte> body)
		{
			var content = Encoding.UTF8.GetString(body.ToArray());
			int currentRetry = 0;

			for (;;) {
				try {
					ProcessMessage(content); // consider async processing
					break;
				}
				catch(Exception) {
					currentRetry++;
					if (currentRetry > _retryCount) {
						_channel.BasicAck(deliveryTag, multiple: false); //or send to dead-letter queue, or mark as failed message and do smth
						throw;
					}
				}

				Thread.Sleep(_delayInSeconds); // consider async version and Task.Delay
			}

			_channel.BasicAck(deliveryTag, multiple: false);
		}
	}
}
