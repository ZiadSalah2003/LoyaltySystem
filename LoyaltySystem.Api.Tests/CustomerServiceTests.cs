using LoyaltySystem.Api.Data;
using LoyaltySystem.Api.Models;
using LoyaltySystem.Api.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltySystem.Api.Tests
{
	public class CustomerServiceTests
	{
		private readonly CustomerService _service;
		private readonly ApplicationDbContext _context;

		public CustomerServiceTests()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "Test")
				.Options;

			_context = new ApplicationDbContext(options);
			_service = new CustomerService(_context);
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

		[Fact]
		public async Task UpdateAsync_ShouldReturnFalse_WhenCustomerDoesNotExist()
		{
			var result = await _service.UpdateAsync(1, 10, CancellationToken.None);

			Assert.False(result);
		}

		[Fact]
		public async Task UpdateAsync_ShouldReturnFalse_WhenPointExceedsCustomerPoint()
		{
			var customer = new Customer { Id = 1, Name = "Test Customer", Email = "test@example.com", Point = 5 };
			_context.Customers.Add(customer);
			await _context.SaveChangesAsync();

			var result = await _service.UpdateAsync(1, 10, CancellationToken.None);

			Assert.False(result);
			Assert.Equal(5, customer.Point);
		}

		[Fact]
		public async Task UpdateAsync_ShouldReturnTrue_WhenPointIsDeductedSuccessfully()
		{
			var customer = new Customer { Id = 1, Name = "Test Customer", Email = "test@example.com", Point = 10 };
			_context.Customers.Add(customer);
			await _context.SaveChangesAsync();

			var result = await _service.UpdateAsync(1, 5, CancellationToken.None);

			Assert.True(result);
			Assert.Equal(5, customer.Point);
		}
	}
}
