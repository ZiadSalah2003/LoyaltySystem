namespace LoyaltySystem.Api.Repositories
{
	public interface IBaseRepository<T> where T : class
	{
		Task<T?> GetCustomerAsync(int id, CancellationToken cancellationToken = default);
	}
}
