using Domain.CategoryAggregate;
using Domain.SeedWork;

namespace Infrastructure.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;
		private bool _disposed;
		private ICategoryRepository _categoryRepository;
		private IItemRepository _itemRepository;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public ICategoryRepository CategoryRepository
		{
			get
			{
				if (_categoryRepository == null) {
					_categoryRepository = new CategoryRepository(_context);
				}
				return _categoryRepository;
			}
		}

		public IItemRepository ItemRepository
		{
			get
			{
				if (_itemRepository == null) {
					_itemRepository = new ItemRepository(_context);
				}
				return _itemRepository;
			}
		}

		public Task<int> CommitAsync()
		{
			return _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~UnitOfWork()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
				if (disposing) {
					_context.Dispose();
				}
			_disposed = true;
		}
	}
}
