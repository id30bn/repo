using Application.Interfaces;
using Application.Models;
using MessageBroker.Shared.Interfaces;

namespace Application.Services
{
	public class NotificationService : INotificationService
	{
		private readonly IRabbitMqSenderService _rabbitMqSender;

		public NotificationService(IRabbitMqSenderService rabbitMqSender)
		{
			_rabbitMqSender = rabbitMqSender;
		}

		void INotificationService.NotifyProductUpdated(GetItemModel item)
		{
			_rabbitMqSender.SendMessage(item);
		}
	}
}
