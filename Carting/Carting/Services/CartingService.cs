using Carting.Core.Interfaces;
using Carting.Core.Models.Cart;

namespace Carting.Services
{
	public class CartingService : ICartingService
	{
		private readonly ICartingRepository _repository;
		public CartingService(ICartingRepository repository)
		{
			_repository = repository;
		}

		public ICollection<Item> GetCartItems(int cartId)
		{
			return _repository.GetCartItems(cartId);
		}

		public void AddItemToCart(int cartId, Item item)
		{
			_repository.AddItemToCart(cartId, item);
		}

		public Item RemoveItemFromCart(int cartId, int itemId)
		{
			return _repository.DeleteCartItem(cartId, itemId);
		}

		public void CreateCart(Cart cart)
		{
			_repository.AddCart(cart);
		}

		public Cart GetCart(int cartId)
		{
			return _repository.GetCart(cartId);
		}

		public ICollection<Item> GetAllItems()
		{
			return _repository.GetAllItems();
		}

		public Item UpdateItem(Item newItem)
		{
			return _repository.UpdateItem(newItem);
		}
	}
}
