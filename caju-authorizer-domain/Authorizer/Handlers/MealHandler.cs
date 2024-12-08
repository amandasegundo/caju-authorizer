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
        Console.Error.WriteLine("O saldo para Meal é insuficiente para esse valor, o valor do Cash será verificado");
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
        Console.Error.WriteLine($"Foi descontado {account.MealBalance} no saldo de Meal e {rest} no saldo do Cash");

        var newCashBalance = CalculateNewBalance(account.CashBalance, rest);
        account.CashBalance = newCashBalance;
        account.MealBalance = 0;
        accountRepository.UpdateAccount(authorizerRequest.Account, account);

        return ResponseCodes.Approved.GetDescription();
      }

      Console.Error.WriteLine("O valor do Cash também não foi suficiente");

      return ResponseCodes.Rejected.GetDescription();
    }
  }
}
