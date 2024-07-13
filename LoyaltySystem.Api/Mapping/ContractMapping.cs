using LoyaltySystem.Api.Contracts;

namespace LoyaltySystem.Api.Mapping
{
	public static class ContractMapping
	{
		public static CustomerResponse MapToCustomerResponse(this Customer customer)
		{
			return new()
			{
				Id= customer.Id,
				Name= customer.Name,
				Email= customer.Email,
				Point= customer.Point,
			};
		}
	}
}
