﻿using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Carting.Setup
{
	public class ConfigureSwaggerOptions: IConfigureNamedOptions<SwaggerGenOptions>
	{
		private readonly IApiVersionDescriptionProvider _apiVersionProvider;

		public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionProvider)
		{
			_apiVersionProvider = apiVersionProvider;
		}

		public void Configure(string name, SwaggerGenOptions options)
		{
			Configure(options);
		}

		public void Configure(SwaggerGenOptions options)
		{
			// add swagger document (OpenAPI) for every API version discovered
			foreach (var description in _apiVersionProvider.ApiVersionDescriptions) {
				options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));

				// XML-Docs support
				var outputDocXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var filePath = Path.Combine(AppContext.BaseDirectory, outputDocXmlFile);
				options.IncludeXmlComments(filePath);
			}
		}

		private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
		{
			var info = new OpenApiInfo() {
				Title = "Carting service Web API",
				Version = description.ApiVersion.ToString()
			};

			return info;
		}
	}
}
