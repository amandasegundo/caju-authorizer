using caju_authorizer_domain.Authorizer.Dtos;

namespace caju_authorizer_domain.Authorizer.Handlers.Interfaces
{
  public interface IAuthorizerHandlerFactory
  {
    public AuthorizerHandler Create(AuthorizerRequest transaction);
  }
}
