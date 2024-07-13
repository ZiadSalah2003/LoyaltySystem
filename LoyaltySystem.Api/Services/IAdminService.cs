namespace LoyaltySystem.Api.Services
{
	public interface IAdminService
	{
		Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken=default);
		Task<bool> AddPointAsync(int id, int point, CancellationToken cancellationToken = default);
		//Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken=default);
	}
}
