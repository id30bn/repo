namespace Application.Models
{
	public class CategoryDTO
	{
		public int? Id { get; set; }

		public string Name { get; set; }

		public string? ImageUrl { get; set; }

		public int? ParentId { get; set; }

		public CategoryDTO? Parent { get; set; }
	}
}
