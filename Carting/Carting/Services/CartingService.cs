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

		public async Task<ICollection<Item>> GetCartItemsAsync(int cartId)
		{
			return await _repository.GetCartItemsAsync(cartId);
		}

		public async Task AddItemToCartAsync(int cartId, Item item)
		{
			await _repository.AddItemToCartAsync(cartId, item);
		}

		public async Task RemoveItemFromCartAsync(int cartId, int itemId)
		{
			await _repository.DeleteCartItemAsync(cartId, itemId);
		}

		public async Task CreateCartAsync(Cart cart)
		{
			await _repository.AddCartAsync(cart);
		}
	}
}
