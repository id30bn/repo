using Domain.SeedWork;

namespace Domain.CategoryAggregate
{
	public class Description : ValueObject
	{
		public string Text { get; private set; }

		public bool IsHtml { get; private set; }

		public Description(string text)
		{
			Text = text ?? throw new ArgumentNullException(nameof(text));
			// IsHtml = text.IsHtml(); 
		}


		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Text;
		}
	}
}
