using LoyaltySystem.Api.Data;
using LoyaltySystem.Api.Models;
using LoyaltySystem.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;


namespace LoyaltySystem.Api.Tests
{
	public class AdminsServiceTests
	{
		private readonly AdminService _service;
		private readonly ApplicationDbContext _context;
		public AdminsServiceTests()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "Test")
				.Options;

			_context = new ApplicationDbContext(options);
			_service = new AdminService(_context);
		}
		[Fact]
		public async Task CreateAsync_ShouldAddCustomerToDatabase()
		{
			var customer = new Customer { Id = 1, Name = "Test Customer", Email = "test@example.com", Point = 0 };

			var result = await _service.CreateAsync(customer, CancellationToken.None);

			Assert.Equal(customer, result);
			Assert.Contains(_context.Customers, c => c.Id == customer.Id);
		}

		[Fact]
		public async Task AddPointAsync_ShouldReturnTrue_WhenCustomerExists()
		{
			var customer = new Customer { Id = 1, Name = "Test Customer", Email = "test@example.com", Point = 0 };
			_context.Customers.Add(customer);
			await _context.SaveChangesAsync();

			var result = await _service.AddPointAsync(1, 10, CancellationToken.None);

			Assert.True(result);
			Assert.Equal(10, customer.Point);
		}

		[Fact]
		public async Task AddPointAsync_ShouldReturnFalse_WhenCustomerDoesNotExist()
		{
			var result = await _service.AddPointAsync(1, 10, CancellationToken.None);

			Assert.False(result);
		}

		[Fact]
		public async Task GetCustomerAsync_ShouldReturnCustomer_WhenCustomerExists()
		{
			var customer = new Customer { Id = 1, Name = "Test Customer", Email = "test@example.com", Point = 0 };
			_context.Customers.Add(customer);
			await _context.SaveChangesAsync();

			var result = await _service.GetCustomerAsync(1, CancellationToken.None);

			Assert.Equal(customer, result);
		}

		[Fact]
		public async Task GetCustomerAsync_ShouldReturnNull_WhenCustomerDoesNotExist()
		{
			var result = await _service.GetCustomerAsync(1, CancellationToken.None);

			Assert.Null(result);
		}
	}
}