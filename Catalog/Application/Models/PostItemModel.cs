namespace Application.Models
{
	/// <summary>
	/// Item to create (aka Product)
	/// </summary>
	public class PostItemModel
	{
		public string Name { get; set; }

		public string? Description { get; set; }

		public string? ImageUrl { get; set; }

		/// <summary>
		/// Category ID which item belongs to
		/// </summary>
		public int CategoryId { get; set; }

		public decimal Price { get; set; }

		public int Amount { get; set; }
	}
}
