using System.Diagnostics.CodeAnalysis;

namespace caju_authorizer_domain.Authorizer.Entities
{
  [ExcludeFromCodeCoverage]
  public class Account
  {
    public required string Id { get; set; }
    public decimal FoodBalance { get; set; }
    public decimal MealBalance { get; set; }
    public decimal CashBalance { get; set; }
  }
}
