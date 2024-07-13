namespace LoyaltySystem.Api.Contracts
{
	public class CustomerResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		[MaxLength(200), Required]
		public string? Email { get; set; }
		public int Point { get; set; }
	}
}
