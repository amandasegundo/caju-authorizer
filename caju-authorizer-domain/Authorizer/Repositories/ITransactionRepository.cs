
using caju_authorizer_domain.Authorizer.Entities;

namespace caju_authorizer_domain.Authorizer.Repositories
{
  public interface ITransactionRepository
  {
    public IEnumerable<Transaction> GetTransactionsByAccountId(string accountId);
    public void InsertTransaction(Transaction transaction);
  }
}
