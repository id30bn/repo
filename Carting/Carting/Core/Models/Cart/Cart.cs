namespace Carting.Core.Models.Cart
{
    public class Cart
    {
        public int Id { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>(); 
    }
}
