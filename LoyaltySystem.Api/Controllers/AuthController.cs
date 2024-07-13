using LoyaltySystem.Api.Contracts.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltySystem.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		public AuthController(IAuthService authService)
        {
			_authService = authService;

		}
        [HttpPost("Login")]
		public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
		{
			var authResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
			return authResult is null ? BadRequest("Invalid Email Or Password") : Ok(authResult);
		}
		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
		{
			var authResult = await _authService.RegisterAsync(request, cancellationToken);
			return authResult is null ? BadRequest("Registration Failed") : Ok(authResult);
		}
	}
}
