namespace Application.Models
{
	/// <summary>
	/// Item to get (aka Product)
	/// </summary>
	public class GetItemModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string? Description { get; set; }

		public bool? IsHtmlDescription { get; set; }

		public string? ImageUrl { get; set; }

		/// <summary>
		/// Category ID which item belongs to
		/// </summary>
		public int CategoryId { get; set; }

		/// <summary>
		/// Category which item belongs to
		/// </summary>
		public GetCategoryModel Category { get; set; }

		public decimal Price { get; set; }

		public int Amount { get; set; }
	}
}
