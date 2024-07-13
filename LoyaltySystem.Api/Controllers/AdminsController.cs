using LoyaltySystem.Api.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace LoyaltySystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize]
	public class AdminsController : ControllerBase
	{
		private readonly IAdminService _adminService;
		private readonly IBaseRepository<Customer> _baseRepository;
		public AdminsController(IAdminService adminService, IBaseRepository<Customer> baseRepository)
		{
			_adminService = adminService;
			_baseRepository = baseRepository;
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
	}
}
