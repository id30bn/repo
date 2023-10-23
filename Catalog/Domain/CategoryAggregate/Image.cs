using Domain.SeedWork;

namespace Domain.CategoryAggregate
{
    public class Image : ValueObject
    {
        public string Url { get; private set; }

        public Image(string url)
        {
            if(url == null) {  
                throw new ArgumentNullException(nameof(url));
            }
            // validate url here
            Url = url;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
        }
    }
}
