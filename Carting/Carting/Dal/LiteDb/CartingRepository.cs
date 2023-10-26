using Carting.Core.Interfaces;
using Carting.Core.Models.Cart;
using LiteDB;

namespace Carting.Dal.LiteDb
{
	public class CartingRepository : ICartingRepository
	{
		private readonly LiteDatabase _database;

		public CartingRepository(ILiteDbContext context)
		{
			_database = context.Database;
		}

		public void AddItemToCart(int cartId, Item item)
		{
			var cart = _database.GetCollection<Cart>().FindById(cartId) ?? new Cart(cartId);

			if (_database.GetCollection<Item>().FindById(item.Id) == null) {
				_database.GetCollection<Item>().Insert(item);
			}

			cart.Items.Add(item);
			_database.GetCollection<Cart>().Delete(cartId);
			_database.GetCollection<Cart>().Insert(cart);
		}

		public Item DeleteCartItem(int cartId, int itemId)
		{
			var cart = _database.GetCollection<Cart>().FindById(cartId);
			if (cart == null) {
				return null;
			}

			var itemToDelete = cart.Items.FirstOrDefault(x => x.Id == itemId);
			if (itemToDelete == null) {
				return null;
			}
			cart.Items.Remove(itemToDelete);
			_database.GetCollection<Cart>().Update(cart);
			// other carts may have this item
			//_database.GetCollection<Item>().Delete(itemToDelete.Id);

			return itemToDelete;
		}

		public void AddCart(Cart cart)
		{
			_database.GetCollection<Cart>().Insert(cart);
			_database.GetCollection<Item>().Insert(cart.Items);
		}

		public Cart GetCart(int cartId)
		{
			return _database.GetCollection<Cart>()
				.Query()
				.Include(x => x.Items)
				.Where(x => x.Id == cartId)
				.FirstOrDefault();
		}

		public ICollection<Item> GetCartItems(int cartId)
		{
			return GetCart(cartId)?.Items;
		}
	}
}
