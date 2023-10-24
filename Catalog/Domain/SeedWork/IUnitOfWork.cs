using Domain.CategoryAggregate;

namespace Domain.SeedWork
{
	public interface IUnitOfWork: IDisposable
	{
		ICategoryRepository CategoryRepository { get; }

		IItemRepository ItemRepository { get; }

		Task<int> CommitAsync();
	}
}
