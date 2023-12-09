using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("ocelot.json");

//var identityUrl = _cfg.GetValue<string>("IdentityUrl"); //from config 
var authenticationProviderKey = "BearerScheme";

builder.Services.AddAuthentication(authenticationProviderKey)
	.AddJwtBearer(authenticationProviderKey, config => {
		config.RequireHttpsMetadata = false;
		config.MetadataAddress = "http://localhost:8080/realms/course/.well-known/openid-configuration";
		config.TokenValidationParameters = new TokenValidationParameters() {
			ValidAudiences = new[] { "catalog", "carting" } // bearer token will contain such audience
		};
	});

builder.Services.AddOcelot(builder.Configuration)
	.AddCacheManager(x => x.WithDictionaryHandle());
//.AddTransientDefinedAggregator<FakeDefinedAggregator>();

var app = builder.Build();

app.UseOcelot().Wait();

app.Run();
