using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Handlers.Interfaces;
using caju_authorizer_domain.Authorizer.Repositories;

namespace caju_authorizer_domain.Authorizer.Handlers
{
  public class AuthorizerHandlerFactory(
    IAccountRepository accountRepository
  ) : IAuthorizerHandlerFactory
  {
    public AuthorizerHandler Create(AuthorizerRequest authorizerRequest)
    {
      var account = accountRepository.GetAccount(authorizerRequest.Account);

      if (account is null)
      {
        throw new InvalidOperationException("Conta não encontrada no banco de dados");
      }

      return authorizerRequest.MCC switch
      {
        "5411" or "5412" => new FoodHandler(authorizerRequest, account, accountRepository),
        "5811" or "5812" => new MealHandler(authorizerRequest, account, accountRepository),
        _ => new CashHandler(authorizerRequest, account, accountRepository),
      };
    }
  }
}
