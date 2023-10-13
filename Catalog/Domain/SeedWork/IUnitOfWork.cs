using Domain.CategoryAggregate;

namespace Domain.SeedWork
{
	public interface IUnitOfWork: IDisposable
	{
		IRepository<Category> CategoryRepository { get; }

		IRepository<Item> ItemRepository { get; }

		Task<int> CommitAsync();
	}
}
