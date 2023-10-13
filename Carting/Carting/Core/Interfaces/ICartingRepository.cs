using Carting.Core.Models.Cart;

namespace Carting.Core.Interfaces
{
	public interface ICartingRepository
	{
		void AddCart(Cart cart);

		ICollection<Item> GetCartItems(int cartId);

		void AddItemToCart(int cartId, Item item);

		void DeleteCartItem(int cartId, int itemId);
	}
}
