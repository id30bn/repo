using Microsoft.AspNetCore.Mvc;

namespace Application.Models
{
	public class PageQueryParams
	{
		private const int DefaultPageNumber = 1;
		private const int DefaultPageSize = 3;

		/// <summary>
		/// A page number with a number 1 or greater
		/// </summary>
		[FromQuery(Name = "page")]
		public int Page { get; set; } = DefaultPageNumber;

		/// <summary>
		/// A page size with a number 1 or greater. Represents the number of items returned per page.
		/// </summary>
		[FromQuery(Name = "limit")]
		public int Limit { get; set; } = DefaultPageSize;
	}
}
