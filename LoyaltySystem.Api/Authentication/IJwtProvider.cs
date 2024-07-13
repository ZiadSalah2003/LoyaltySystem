using Microsoft.AspNetCore.Identity;

namespace LoyaltySystem.Api.Authentication
{
	public interface IJwtProvider
	{
		(string token, int expiresIn) GenerateToken(IdentityUser user);
    }
}
