using caju_authorizer_domain.Abstractions.Extensions;
using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Enums;
using caju_authorizer_domain.Authorizer.Repositories;

namespace caju_authorizer_domain.Authorizer.Handlers
{
  public class FoodHandler(
    AuthorizerRequest authorizerRequest,
    IAccountRepository accountRepository) : AuthorizerHandler
  {
    public override string Handle()
    {
      var account = accountRepository.GetAccount(authorizerRequest.Account);

      if (!HasSufficientBalance(account.FoodBalance, authorizerRequest.TotalAmount))
      {
        return ResponseCodes.Rejected.GetDescription();
      }

      var newBalance = CalculateNewBalance(account.FoodBalance, authorizerRequest.TotalAmount);
      account.FoodBalance = newBalance;
      accountRepository.UpdateAccount(authorizerRequest.Account, account);

      return ResponseCodes.Approved.GetDescription();
    }
  }
}
