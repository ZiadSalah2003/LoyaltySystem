using LoyaltySystem.Api.Authentication;
using LoyaltySystem.Api.Contracts.Authentication;
using Microsoft.AspNetCore.Identity;

namespace LoyaltySystem.Api.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<IdentityUser> _identityUser;
		private readonly IJwtProvider _jwtProvider;

		public AuthService(UserManager<IdentityUser> identityUser, IJwtProvider jwtProvider)
		{
			_identityUser = identityUser;
			_jwtProvider = jwtProvider;
		}
		public async Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
		{
			var user = await _identityUser.FindByEmailAsync(email);
			if (user is null)
				return null;
			var userPassword = await _identityUser.CheckPasswordAsync(user, password);
			if (!userPassword)
				return null;
			var (token, expiresIn) = _jwtProvider.GenerateToken(user);
			return new AuthResponse(user.Id, user.Email, token, expiresIn);
		}

		public async Task<AuthResponse?> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
		{
			var emailExist = await _identityUser.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
			if (emailExist)
				return null;

			var user = new IdentityUser { UserName = request.Email, Email = request.Email };
			var result = await _identityUser.CreateAsync(user, request.Password);
			if (!result.Succeeded)
				return null;

			var (token, expiresIn) = _jwtProvider.GenerateToken(user);
			return new AuthResponse(user.Id, user.Email, token, expiresIn);
		}
	}
}
