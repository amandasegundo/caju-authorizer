namespace caju_authorizer_domain.Authorizer.Entities
{
  public class Transaction
  {
    public required Guid Id { get; set; }
    public required DateTime Date { get; set; }
    public required string AccountId { get; set; }
    public required decimal Amount { get; set; }
    public required string MCC { get; set; }
    public required string Merchant { get; set; }
    public required string Status { get; set; }
    public required string Detail { get; set; }
  }
}
