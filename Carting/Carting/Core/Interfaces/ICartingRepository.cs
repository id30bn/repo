using Carting.Core.Models.Cart;

namespace Carting.Core.Interfaces
{
	public interface ICartingRepository
	{
		Task AddCartAsync(Cart cart);

		Task<ICollection<Item>> GetCartItemsAsync(int cartId);

		Task AddItemToCartAsync(int cartId, Item item);

		Task DeleteCartItemAsync(int cartId, int itemId);
	}
}
