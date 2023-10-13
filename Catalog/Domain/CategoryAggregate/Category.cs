using Domain.SeedWork;

namespace Domain.CategoryAggregate
{
	public class Category : Entity, IAggregateRoot
	{
		protected Category() { }

		public Category(string name, int? parentId = null, string imageUrl = null)
		{
			if(string.IsNullOrWhiteSpace(name)) {
				throw new ArgumentException(nameof(name));
			}

			Name = name;
			Image = (imageUrl == null) ? null : new Image(imageUrl);
			ParentId = parentId;
		}

		public string Name { get; private set; }

		public Image? Image { get; private set; }

		public int? ParentId { get; private set; }
		public Category? Parent { get; private set; }
	}
}
