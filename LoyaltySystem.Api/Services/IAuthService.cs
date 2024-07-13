using LoyaltySystem.Api.Contracts.Authentication;

namespace LoyaltySystem.Api.Services
{
	public interface IAuthService
	{
		Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
		Task<AuthResponse?> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
	}
}
