using caju_authorizer_domain.Abstractions.Extensions;
using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Enums;
using caju_authorizer_domain.Authorizer.Repositories;

namespace caju_authorizer_domain.Authorizer.Handlers
{
  public class CashHandler(
    AuthorizerRequest authorizerRequest,
    Account account,
    IAccountRepository accountRepository) : AuthorizerHandler
  {
    public override string Handle()
    {
      if (!HasSufficientBalance(account.CashBalance, authorizerRequest.TotalAmount))
      {
        return ResponseCodes.Rejected.GetDescription();
      }

      var newBalance = CalculateNewBalance(account.CashBalance, authorizerRequest.TotalAmount);
      account.CashBalance = newBalance;
      accountRepository.UpdateAccount(authorizerRequest.Account, account);

      return ResponseCodes.Approved.GetDescription();
    }
  }
}
