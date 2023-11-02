using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.CategoryAggregate;
using Domain.Exceptions;
using Domain.SeedWork;

namespace Application.Services
{
	// item aka product
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _uof;
		private readonly IMapper _mapper;
		private readonly INotificationService _notificationService;

		public ProductService(IUnitOfWork uof, IMapper mapper, INotificationService notificationService)
		{
			_uof = uof;
			_mapper = mapper;
			_notificationService = notificationService;
		}

		public async Task<GetItemModel> CreateAsync(PostItemModel item)
		{
			var category = await _uof.CategoryRepository.GetByIdAsync(item.CategoryId);
			if (category == null) {
				throw new EntityNotFoundException($"Cannot find category by id {item.CategoryId}");
			}

			var domain = _uof.ItemRepository.Add(_mapper.Map<Item>(item));
			await _uof.CommitAsync();
			return _mapper.Map<GetItemModel>(domain);
		}

		public async Task<GetItemModel> DeleteAsync(int id)
		{
			var domain = await _uof.ItemRepository.GetByIdAsync(id);
			if (domain != null) {
				_uof.ItemRepository.Delete(domain);
				await _uof.CommitAsync();
			}

			return _mapper.Map<GetItemModel>(domain);
		}

		public async Task<IEnumerable<GetItemModel>> FindListAsync(ItemQueryParams queryParams)
		{
			int skip = queryParams.Limit * (queryParams.Page - 1);
			int take = queryParams.Limit;

			var domains = await _uof.ItemRepository
				.FindListAsync(x => x.CategoryId == queryParams.CategoryId, skip, take);
			return _mapper.Map<IEnumerable<Item>, IEnumerable<GetItemModel>>(domains);
		}

		public async Task<GetItemModel> GetByIdAsync(int id)
		{
			return _mapper.Map<GetItemModel>(await _uof.ItemRepository.GetByIdAsync(id));
		}

		public async Task<IEnumerable<GetItemModel>> ListAsync()
		{
			var domains = await _uof.ItemRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<Item>, IEnumerable<GetItemModel>>(domains);
		}

		public async Task<GetItemModel> UpdateAsync(int id, PostItemModel item)
		{
			var category = await _uof.CategoryRepository.GetByIdAsync(item.CategoryId);
			if (category == null) {
				throw new EntityNotFoundException($"Cannot find category by id {item.CategoryId}");
			}

			var result = await _uof.ItemRepository.UpdateAsync(id, _mapper.Map<Item>(item));
			GetItemModel mappedResult = null;

			if (result != null) {
				await _uof.CommitAsync();
				mappedResult = _mapper.Map<GetItemModel>(result);
				_notificationService.NotifyProductUpdated(mappedResult);
			}

			return mappedResult;
		}
	}
}
