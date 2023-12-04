using Application.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("auth")]
	// for manual testing only to get access token
	public class AuthenticationController : ControllerBase
	{
		private readonly IHttpClientFactory _clientFactory;

		public AuthenticationController(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		[HttpPost]
		// to get access token
		public async Task<ActionResult> Post(ClientCredentialsModel creds)
		{
			var authServerClient = _clientFactory.CreateClient();
			var tokenResponse = await authServerClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest {
				Address = $"http://localhost:8080/realms/{creds.Realm}/protocol/openid-connect/token",
				ClientId = creds.ClientId,
				ClientSecret = creds.ClientSecret,
				Scope = string.Join(" ", creds.Scopes)
			});

			// example: call API with access token
			var apiClient = _clientFactory.CreateClient();
			apiClient.SetBearerToken(tokenResponse.AccessToken);
			var response = await apiClient.GetAsync("https://localhost:7178/categories");
			var content = await response.Content.ReadAsStringAsync();

			return Ok(new { access_token = tokenResponse.AccessToken, content });
		}

		// request example 
		//{
		//	"clientId": "catalog_client_id",
		//  "clientSecret": "secret",
		//  "scopes": [
		//	"catalog", "create"
		//  ],
		//  "realm": "course"
		//}
	}
}
