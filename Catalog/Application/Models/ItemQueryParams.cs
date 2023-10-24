using Microsoft.AspNetCore.Mvc;

namespace Application.Models
{
	public class ItemQueryParams : PageQueryParams
	{
		/// <summary>
		/// Category ID
		/// </summary>
		[FromQuery(Name = "categoryId")]
		public int CategoryId { get; set; }
	}
}
