namespace LoyaltySystem.Api.Services
{
	public interface ICustomerService
	{
		Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken = default);
		Task<bool> UpdateAsync(int id, int point, CancellationToken cancellationToken = default);
	}
}
