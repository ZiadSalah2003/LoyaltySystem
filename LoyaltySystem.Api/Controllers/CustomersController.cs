using LoyaltySystem.Api.Mapping;
using LoyaltySystem.Api.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace LoyaltySystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class CustomersController : ControllerBase
	{
		private readonly ICustomerService _customerService;
		private readonly IBaseRepository<Customer> _baseRepository;
		public CustomersController(ICustomerService customerService, IBaseRepository<Customer> baseRepository)
		{
			_customerService = customerService;
			_baseRepository = baseRepository;
		}
		[Authorize]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCustomer([FromRoute] int id, CancellationToken cancellationToken)
		{
			var item = await _baseRepository.GetCustomerAsync(id, cancellationToken);
			return item is null ? NotFound() : Ok(item.MapToCustomerResponse());
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, int point, CancellationToken cancellationToken)
		{
			var isUpdated = await _customerService.UpdateAsync(id, point, cancellationToken);
			if (!isUpdated)
				return BadRequest("Your Point is less than Point the are you put Or You Put Invalid Id");

			return NoContent();
		}
	}
}
