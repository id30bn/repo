using Carting.Core.Models.Cart;

namespace Carting.Core.Interfaces
{
	public interface ICartingRepository
	{
		void AddCart(Cart cart);

		Cart GetCart(int cartId);

		ICollection<Item> GetCartItems(int cartId);

		void AddItemToCart(int cartId, Item item);

		Item DeleteCartItem(int cartId, int itemId);
	}
}
