namespace LoyaltySystem.Api.Contracts.Authentication
{
	public class RegisterRequest
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public RegisterRequest(string email, string password)
		{
			Email = email;
			Password = password;
		}
	}
}
