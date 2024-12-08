using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Repositories;
using caju_authorizer_domain.Authorizer.Services.Interfaces;

namespace caju_authorizer_domain.Authorizer.Services
{
  public class TransactionService(ITransactionRepository transactionRepository) : ITransactionService
  {
    public IEnumerable<Transaction> GetTransactionsByAccountId(string accountId)
    {
      return transactionRepository.GetTransactionsByAccountId(accountId);
    }
  }
}
