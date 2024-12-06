using caju_authorizer_domain.Authorizer.Dtos;

namespace caju_authorizer_domain.Authorizer.Services.Interfaces
{
  public interface IAuthorizerService
  {
    public AuthorizerResponse Authorize(AuthorizerRequest transaction);
  }
}
