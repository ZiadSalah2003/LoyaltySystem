using Hangfire;
using LoyaltySystem.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;

namespace LoyaltySystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize]
	public class AdminsController : ControllerBase
	{
		private readonly IAdminService _adminService;
		private readonly IBaseRepository<Customer> _baseRepository;
		private readonly ApplicationDbContext _context;
		private readonly IEmailSender _emailSender;
		public AdminsController(IAdminService adminService, IBaseRepository<Customer> baseRepository, ApplicationDbContext context, IEmailSender emailSender)
		{
			_adminService = adminService;
			_baseRepository = baseRepository;
			_context = context;
			_emailSender = emailSender;
			BackgroundJob.Schedule(() => SendDailyEmailAsync(), TimeSpan.FromMinutes(1));
		}

		[HttpPost("")]
		public async Task<IActionResult> Create([FromBody] Customer customer,CancellationToken cancellationToken)
		{
			
			var newCustomer = await _adminService.CreateAsync(customer, cancellationToken);
			return CreatedAtAction(nameof(GetCustomer), new { id = newCustomer.Id }, newCustomer);
		}
		[HttpPost("{id}")]
		public async Task<IActionResult> AddPoint([FromRoute] int id, int point, CancellationToken cancellationToken)
		{
			var isadded = await _adminService.AddPointAsync(id, point, cancellationToken);
			if (!isadded)
				return NotFound();
			return NoContent();
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCustomer([FromRoute] int id, CancellationToken cancellationToken)
		{
			var customer = await _baseRepository.GetCustomerAsync(id, cancellationToken);
			return customer is null ? NotFound() : Ok(customer);
		}
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task SendDailyEmailAsync()
		{
			var customers = await _context.Customers
										  .Where(c => c.Point < 100)
										  .ToListAsync();

			foreach (var customer in customers)
			{
				var subject = "Daily Points Reminder";
				var htmlMessage = $"Hello {customer.Name},<br>Your current points are {customer.Point}.<br>Best Regards,<br>LoyaltySystem Team";
				await _emailSender.SendEmailAsync(customer.Email!, subject, htmlMessage);
			}
		}
	}
}
