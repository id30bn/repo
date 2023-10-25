using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
	/// <summary>
	/// Item to create (aka Product)
	/// </summary>
	public class PostItemModel
	{
		//[Required] not necessary in this case (compare to "string?")
		public string Name { get; set; }

		public string? Description { get; set; }

		public string? ImageUrl { get; set; }

		/// <summary>
		/// Category ID which item belongs to
		/// </summary>
		// make nullable for Required attribute to work (because 0 is default). Required checks on null only.
		[Range(1, int.MaxValue)]
		public int CategoryId { get; set; }

		public decimal Price { get; set; }

		public int Amount { get; set; }
	}
}
