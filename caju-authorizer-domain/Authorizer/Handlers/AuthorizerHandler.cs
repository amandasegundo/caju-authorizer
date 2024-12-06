using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Repositories;

namespace caju_authorizer_domain.Authorizer.Handlers
{
  public abstract class AuthorizerHandler
  {
    public abstract string Handle();

    public bool HasSufficientBalance(decimal balance, decimal amount) =>  (balance - amount) >= 0;
    public decimal CalculateNewBalance(decimal currentBalance, decimal amount) => (currentBalance - amount);
  }
}
