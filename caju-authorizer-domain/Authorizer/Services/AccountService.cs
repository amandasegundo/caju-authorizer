using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Repositories;
using caju_authorizer_domain.Authorizer.Services.Interfaces;

namespace caju_authorizer_domain.Authorizer.Services
{
  public class AccountService(IAccountRepository accountRepository) : IAccountService
  {
    public Account GetAccount(string id)
    {
      return accountRepository.GetAccount(id);
    }
  }
}
