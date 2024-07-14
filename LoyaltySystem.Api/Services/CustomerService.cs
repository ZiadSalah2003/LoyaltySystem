namespace LoyaltySystem.Api.Services
{
	public class CustomerService : ICustomerService
	{

		private readonly ApplicationDbContext _context;
		public CustomerService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<bool> UpdateAsync(int id, int point, CancellationToken cancellationToken = default)
		{
			var item = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
			if (item is null)
				return false;
			if (point > item.Point)
				return false;
			item.Point -= point;
			item.DateTimes?.Add(DateTime.UtcNow);
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
	}
}
