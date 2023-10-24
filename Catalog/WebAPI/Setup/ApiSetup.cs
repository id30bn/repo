using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace WebAPI.Setup
{
	internal static class ApiSetup
	{
		internal static IServiceCollection ConfigureApi(this IServiceCollection services)
		{
			if (services is null) {
				throw new ArgumentNullException(nameof(services));
			}

			return services
				.AddResponseCaching()
				.ConfigureControllers()
				.ConfigureSwagger();
		}

		internal static IServiceCollection ConfigureControllers(this IServiceCollection services)
		{
			services.AddControllers(options => {
				options.ReturnHttpNotAcceptable = true;
				options.CacheProfiles.Add("DefaultCacheProfile", new CacheProfile() { Duration = 60 }); // in seconds
			}).AddJsonOptions(options => {
				options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
			});

			return services;
		}
	}
}
