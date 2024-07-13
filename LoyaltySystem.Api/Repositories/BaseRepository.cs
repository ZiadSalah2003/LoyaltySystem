using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Api.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;

		public BaseRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<T?> GetCustomerAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Set<T>().FindAsync(id);
		}
	}
}
