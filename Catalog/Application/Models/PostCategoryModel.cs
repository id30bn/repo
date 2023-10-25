using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
	/// <summary>
	/// Category to create
	/// </summary>
	public class PostCategoryModel
	{
		[Required]
		public string Name { get; set; }

		public string? ImageUrl { get; set; }

		/// <summary>
		/// ID of parent category
		/// </summary>
		public int? ParentId { get; set; }
	}
}
