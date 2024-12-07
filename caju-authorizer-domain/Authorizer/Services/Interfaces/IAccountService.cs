using caju_authorizer_domain.Authorizer.Entities;

namespace caju_authorizer_domain.Authorizer.Services.Interfaces
{
  public interface IAccountService
  {
    public IEnumerable<Account> GetAccounts();
    public Account GetAccount(string id);
  }
}
