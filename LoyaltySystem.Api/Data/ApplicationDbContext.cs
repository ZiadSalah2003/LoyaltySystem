using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Api.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public DbSet<Admin>Admins { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}
