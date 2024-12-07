namespace caju_authorizer_domain.Authorizer.Handlers
{
  public abstract class AuthorizerHandler
  {
    public abstract string Handle();

    public bool HasSufficientBalance(decimal balance, decimal amount) =>  (balance - amount) >= 0;
    public decimal CalculateNewBalance(decimal balance, decimal amount) => (balance - amount);
  }
}
