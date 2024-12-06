using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Handlers.Interfaces;
using caju_authorizer_domain.Authorizer.Services.Interfaces;

namespace caju_authorizer_domain.Authorizer.Services
{
  public class AuthorizerService(
    IAuthorizerHandlerFactory authorizerHandlerFactory
  ) : IAuthorizerService
  {
    public AuthorizerResponse Authorize(AuthorizerRequest transaction)
    {
      var responseCode = authorizerHandlerFactory.Create(transaction).Handle();
      var response = new AuthorizerResponse()
      { 
        Code = responseCode
      };
      return response;
    }
  }
}
