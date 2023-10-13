namespace Application.Models
{
	public class Product
	{
		public int? Id { get; set; }

		public string Name { get; set; }

		public string? Description { get; set; }

		public bool? IsHtmlDescription { get; set; }

		public string? ImageUrl { get; set; }

		public int CategoryId { get; set; }

		public CategoryDTO? Category { get; set; }

		public decimal Price { get; set; }

		public int Amount { get; set; }
	}
}
