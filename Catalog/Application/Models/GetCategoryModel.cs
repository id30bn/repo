namespace Application.Models
{
	/// <summary>
	/// Category to get
	/// </summary>
	public class GetCategoryModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string? ImageUrl { get; set; }

		/// <summary>
		/// Parent category ID
		/// </summary>
		public int? ParentId { get; set; }

		/// <summary>
		/// Parent category
		/// </summary>
		public GetCategoryModel? Parent { get; set; }
	}
}
