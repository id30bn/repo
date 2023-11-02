using Application.Interfaces;
using Application.Models.Mappers;
using Application.Services;
using MessageBroker.Shared.Interfaces;
using MessageBroker.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public static class ApplicationSetup
	{
		public static IServiceCollection ConfigureApplication(this IServiceCollection services)
		{
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IProductService, ProductService>();

			services.AddScoped<IRabbitMqSenderService, RabbitMqSenderService>();
			services.AddScoped<INotificationService, NotificationService>();

			services.AddAutoMapper(typeof(CategoryMapperProfile).Assembly);

			return services;
		}
	}
}
