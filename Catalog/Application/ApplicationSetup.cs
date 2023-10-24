using Application.Interfaces;
using Application.Models.Mappers;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public static class ApplicationSetup
	{
		public static IServiceCollection ConfigureApplication(this IServiceCollection services)
		{
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IProductService, ProductService>();

			services.AddAutoMapper(typeof(CategoryMapperProfile).Assembly);

			return services;
		}
	}
}
