namespace LoyaltySystem.Api.Contracts.Authentication
{
	public class AuthResponse
	{
		public string Id { get; init; }
		public string? Email { get; init; }
		public string Token { get; init; }
		public int ExpiresIn { get; init; }

		public AuthResponse(string id, string? email, string token, int expiresIn)
		{
			Id = id;
			Email = email;
			Token = token;
			ExpiresIn = expiresIn;
		}
	}
}
