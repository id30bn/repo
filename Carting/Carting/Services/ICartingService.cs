using Carting.Core.Models.Cart;

namespace Carting.Services
{
	public interface ICartingService
	{
		Task CreateCartAsync(Cart cart);

		Task<ICollection<Item>> GetCartItemsAsync(int cartId);

		Task AddItemToCartAsync(int cartId, Item item);

		Task RemoveItemFromCartAsync(int cartId, int itemId);
	}
}
