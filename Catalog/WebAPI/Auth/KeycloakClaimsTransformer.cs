using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Security.Claims;

namespace WebAPI.Auth
{
	public class KeycloakClaimsTransformer : IClaimsTransformation
	{
		public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
		{
			ClaimsIdentity claimsIdentity = (ClaimsIdentity)principal.Identity;
			const string RealmClaimName = "realm_access";

			if (claimsIdentity.IsAuthenticated && claimsIdentity.HasClaim(claim => claim.Type == RealmClaimName)) {
				var realmClaim = claimsIdentity.FindFirst(claim => claim.Type == RealmClaimName);
				var realmClaimAsDictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(realmClaim.Value);
				if (realmClaimAsDictionary["roles"] != null) {
					foreach (var role in realmClaimAsDictionary["roles"]) {
						claimsIdentity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
					}
				}
			}

			return Task.FromResult(principal);
		}
	}
}
