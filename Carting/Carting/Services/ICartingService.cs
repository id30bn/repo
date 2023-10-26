using Carting.Core.Models.Cart;

namespace Carting.Services
{
	public interface ICartingService
	{
		void CreateCart(Cart cart);

		Cart GetCart(int cartId);

		ICollection<Item> GetCartItems(int cartId);

		void AddItemToCart(int cartId, Item item);

		Item RemoveItemFromCart(int cartId, int itemId);
	}
}
