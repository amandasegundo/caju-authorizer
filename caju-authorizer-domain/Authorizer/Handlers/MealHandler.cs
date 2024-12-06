using caju_authorizer_domain.Abstractions.Extensions;
using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Enums;
using caju_authorizer_domain.Authorizer.Repositories;

namespace caju_authorizer_domain.Authorizer.Handlers
{
  public class MealHandler(
    AuthorizerRequest authorizerRequest,
    IAccountRepository accountRepository) : AuthorizerHandler
  {
    public override string Handle()
    {
      var account = accountRepository.GetAccount(authorizerRequest.Account);

      if (!HasSufficientBalance(account.MealBalance, authorizerRequest.TotalAmount))
      {
        return ResponseCodes.Rejected.GetDescription();
      }

      var newBalance = CalculateNewBalance(account.MealBalance, authorizerRequest.TotalAmount);
      account.MealBalance = newBalance;
      accountRepository.UpdateAccount(authorizerRequest.Account, account);

      return ResponseCodes.Approved.GetDescription();
    }
  }
}
