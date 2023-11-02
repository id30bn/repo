using Application.Models;

namespace Application.Interfaces
{
	public interface INotificationService
	{
		void NotifyProductUpdated(GetItemModel item);
	}
}
