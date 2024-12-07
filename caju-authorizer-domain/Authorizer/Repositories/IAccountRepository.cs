using caju_authorizer_domain.Authorizer.Entities;

namespace caju_authorizer_domain.Authorizer.Repositories
{
  public interface IAccountRepository
  {
    public Account GetAccount(string id);
    public IEnumerable<Account> GetAccounts();
    public void UpdateAccount(string id, Account account);
  }
}
