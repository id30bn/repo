namespace Domain.Exceptions
{
	public class CategoryNotFoundException: Exception
	{
		public CategoryNotFoundException(string? message = null): base(message) { }
	}
}
