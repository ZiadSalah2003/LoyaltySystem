
namespace LoyaltySystem.Api.Services
{
	public class AdminService : IAdminService
	{
		private readonly ApplicationDbContext _context;
		public AdminService(ApplicationDbContext context)
		{
			_context = context;
		}


		public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default)
		{
			await _context.Customers.AddAsync(customer, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
			return customer;
		}
		public async Task<bool> AddPointAsync(int id, int point, CancellationToken cancellationToken = default)
		{
			var item = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
			if (item is null)
				return false;
			item.Point += point;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		//in test
		public async Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
