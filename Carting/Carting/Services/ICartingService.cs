using Carting.Core.Models.Cart;

namespace Carting.Services
{
	public interface ICartingService
	{
		void CreateCart(Cart cart);

		ICollection<Item> GetCartItems(int cartId);

		void AddItemToCart(int cartId, Item item);

		void RemoveItemFromCart(int cartId, int itemId);
	}
}
