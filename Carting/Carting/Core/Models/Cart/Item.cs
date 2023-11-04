namespace Carting.Core.Models.Cart
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FullDescription { get; set; }

        public Image? Image { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }
    }

}
