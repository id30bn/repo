using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using WebAPI.Auth;
using WebAPI.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IClaimsTransformation, KeycloakClaimsTransformer>();

// Add services to the container.
builder.Services.AddAuthentication("Bearer")
	.AddJwtBearer("Bearer", config => { // or use AddOAuth
		config.RequireHttpsMetadata = false;
		config.MetadataAddress = "http://localhost:8080/realms/course/.well-known/openid-configuration";
		config.Audience = "catalog";
	});
// scopes example: "catalog read update"

// we don't need AddPolicy since we use simple role-based approach with [Authorize] attribute
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureApi();
builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpClient();

var app = builder.Build();
app.UseAuthentication();
app.UseExceptionHandler("/errors");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();

app.Run();
