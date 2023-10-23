using Domain.SeedWork;

namespace Domain.CategoryAggregate
{
	public class Item : Entity
	{
		protected Item() { }

		public Item(string name, int categoryId, decimal price, int amount, string imageUrl = null, string description = null)
		{
			if (string.IsNullOrWhiteSpace(name)) {
				throw new ArgumentException(nameof(name));
			}

			if (amount < 0) {
				throw new ArgumentException(nameof(amount));
			}

			Name = name;
			CategoryId = categoryId;
			Price = price;
			Image = (imageUrl == null) ? null : new Image(imageUrl);
			Description = (description == null) ? null : new Description(description);
			Price = price;
			Amount = amount;
		}

		public string Name { get; private set; }

		public Description? Description { get; private set; }

		public Image? Image { get; private set; }

		public int CategoryId { get; private set; }

		public Category Category { get; private set; }

		public decimal Price { get; private set; }

		public int Amount { get; private set; } // can be just get also
	}
}
