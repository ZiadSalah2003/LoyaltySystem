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
		private readonly IEmailSender _emailSender;
		public AuthController(IAuthService authService, IEmailSender emailSender)
        {
			_authService = authService;
			_emailSender = emailSender;

		}
        [HttpPost("Login")]
		public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
		{
			var authResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
			if (authResult is null)
				return BadRequest("Invalid Email Or Password");
			var subject = "Login Notification";
			var htmlMessage = $"Hello,<br>You have successfully logged in at {DateTime.UtcNow}.<br>Best Regards,<br>LoyaltySystem Team";
			await _emailSender.SendEmailAsync(request.Email, subject, htmlMessage);

			return  Ok(authResult);
		}
		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
		{
			var authResult = await _authService.RegisterAsync(request, cancellationToken);
			return authResult is null ? BadRequest("Registration Failed") : Ok(authResult);
		}
	}
}
