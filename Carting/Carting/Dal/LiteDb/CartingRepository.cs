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

		public ICollection<Item> GetCartItems(int cartId)
		{
			return _database.GetCollection<Cart>()
				.Query()
				.Include(x => x.Items)
				.Where(x => x.Id == cartId)
				.FirstOrDefault()
				?.Items;
		}

		public void AddItemToCart(int cartId, Item item)
		{
			var cart = _database.GetCollection<Cart>().FindById(cartId);
			if (cart != null) {
				_database.GetCollection<Item>().Insert(item);
				cart.Items.Add(item);
				_database.GetCollection<Cart>().Update(cart);
			}
		}

		public void DeleteCartItem(int cartId, int itemId)
		{
			var cart = _database.GetCollection<Cart>().FindById(cartId);
			if (cart != null) {
				var itemToDelete = cart.Items.FirstOrDefault(x => x.Id == itemId);
				if (itemToDelete != null) {
					cart.Items.Remove(itemToDelete);
					_database.GetCollection<Cart>().Update(cart);
					_database.GetCollection<Item>().Delete(itemToDelete.Id);
				}
			}

		}

		public void AddCart(Cart cart)
		{
			_database.GetCollection<Cart>().Insert(cart);
			_database.GetCollection<Item>().Insert(cart.Items);
		}
	}
}
