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
      return authorizerRequest.MCC switch
      {
        "5411" or "5412" => new FoodHandler(authorizerRequest, accountRepository),
        "5811" or "5812" => new MealHandler(authorizerRequest, accountRepository),
        _ => new CashHandler(authorizerRequest, accountRepository),
      };
    }
  }
}
