namespace Carting.Core.Models.Cart
{
    public class Cart
    {
		public Cart(int id)
		{
			Id = id;
		}

		public int Id { get; private set; }

        public ICollection<Item> Items { get; private set; } = new List<Item>(); 
    }
}
