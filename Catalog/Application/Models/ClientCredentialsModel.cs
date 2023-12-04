namespace Application.Models
{
	public class ClientCredentialsModel
	{
		public string ClientId { get; set; }

		public string ClientSecret { get; set; }

		public string[] Scopes { get; set; }

		public string Realm { get; set; }
	}
}
