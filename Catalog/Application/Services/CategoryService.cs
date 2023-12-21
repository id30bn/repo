using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.CategoryAggregate;
using Domain.SeedWork;

namespace Application.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork _uof;
		private readonly IMapper _mapper;
		public CategoryService(IUnitOfWork uof, IMapper mapper)
		{
			_uof = uof;
			_mapper = mapper;
		}

		public async Task<GetCategoryModel> CreateAsync(PostCategoryModel category)
		{
			var domain = _uof.CategoryRepository.Add(_mapper.Map<Category>(category));
			await _uof.CommitAsync();
			return _mapper.Map<GetCategoryModel>(domain);
		}

		public async Task<GetCategoryModel> DeleteAsync(int id)
		{
			var domain = await _uof.CategoryRepository.GetByIdAsync(id);
			if (domain != null) {
				_uof.CategoryRepository.Delete(domain);
				await _uof.CommitAsync();
			}
		 
			return _mapper.Map<GetCategoryModel>(domain);
		}

		public async Task<GetCategoryModel> GetByIdAsync(int? id)
		{
			if(id == null) {
				return null;
			}

			return _mapper.Map<GetCategoryModel>(await _uof.CategoryRepository.GetByIdAsync((int)id));
		}

		public async Task<IEnumerable<GetCategoryModel>> ListAsync()
		{
			var domains = await _uof.CategoryRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<Category>, IEnumerable<GetCategoryModel>>(domains);
		}

		public async Task<GetCategoryModel> UpdateAsync(int id, PostCategoryModel category)
		{
			var result = await _uof.CategoryRepository.UpdateAsync(id, _mapper.Map<Category>(category));
			if (result != null) {
				await _uof.CommitAsync();
			}
			return _mapper.Map<GetCategoryModel>(result);
		}
	}
}
