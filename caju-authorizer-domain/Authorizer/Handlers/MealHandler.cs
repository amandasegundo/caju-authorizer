using caju_authorizer_domain.Abstractions.Extensions;
using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Enums;
using caju_authorizer_domain.Authorizer.Repositories;

namespace caju_authorizer_domain.Authorizer.Handlers
{
  public class MealHandler(
    AuthorizerRequest authorizerRequest,
    Account account,
    IAccountRepository accountRepository) : AuthorizerHandler
  {
    public override string Handle()
    {
      if (!HasSufficientBalance(account.MealBalance, authorizerRequest.TotalAmount))
      {
        return Fallback(account);
      }

      var newBalance = CalculateNewBalance(account.MealBalance, authorizerRequest.TotalAmount);
      account.MealBalance = newBalance;
      accountRepository.UpdateAccount(authorizerRequest.Account, account);

      return ResponseCodes.Approved.GetDescription();
    }

    public string Fallback(Account account)
    {
      var rest = Math.Abs(CalculateNewBalance(account.MealBalance, authorizerRequest.TotalAmount));

      if (HasSufficientBalance(account.CashBalance, rest))
      {
        var newCashBalance = CalculateNewBalance(account.CashBalance, rest);
        account.CashBalance = newCashBalance;
        account.MealBalance = 0;
        accountRepository.UpdateAccount(authorizerRequest.Account, account);
        return ResponseCodes.Approved.GetDescription();
      }

      return ResponseCodes.Rejected.GetDescription();
    }
  }
}
