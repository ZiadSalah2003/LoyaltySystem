using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LoyaltySystem.Api.Models
{
	public class Customer 
	{
		public int Id { get; set; }
        public string? Name { get; set; }
        [MaxLength(200),Required]
		public string? Email { get; set; }
        public int Point { get; set; }
		public List<DateTime>? DateTimes { get; set; } = [];

	}
}
