using Domain.CategoryAggregate;

namespace Infrastructure.Persistence
{
	public class CategoryRepository : EfBaseRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(AppDbContext context) : base(context) { }
	}
}
