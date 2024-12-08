using caju_authorizer_domain.Authorizer.Entities;

namespace caju_authorizer_domain.Authorizer.Services.Interfaces
{
  public interface ITransactionService
  {
    public IEnumerable<Transaction> GetTransactionsByAccountId(string accountId);
  }
}
