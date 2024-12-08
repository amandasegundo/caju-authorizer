using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Repositories;
using caju_authorizer_infra.data.Context;

namespace caju_authorizer_infra.data.Repositories.EntityFramework
{
  public class TransactionRepository: ITransactionRepository
  {
    public CajuDbContext _context;

    public TransactionRepository(CajuDbContext context)
    {
      _context = context;
    }
    public IEnumerable<Transaction> GetTransactionsByAccountId(string accountId) 
      => _context.Transactions.Where(e => e.AccountId == accountId);

    public void InsertTransaction(Transaction transaction)
    {
      _context.Add(transaction);
      _context.SaveChanges();
    }
  }
}
