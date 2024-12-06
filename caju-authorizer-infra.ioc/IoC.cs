using caju_authorizer_domain.Authorizer.Handlers;
using caju_authorizer_domain.Authorizer.Handlers.Interfaces;
using caju_authorizer_domain.Authorizer.Repositories;
using caju_authorizer_domain.Authorizer.Services;
using caju_authorizer_domain.Authorizer.Services.Interfaces;
using caju_authorizer_infra.data.Context;
using caju_authorizer_infra.data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace caju_authorizer_infra.ioc
{
  public static class IoC
  {
    public static void Configure(IServiceCollection services)
    {
      // Factories
      services.AddScoped<IAuthorizerHandlerFactory, AuthorizerHandlerFactory>();

      // Services
      services.AddScoped<IAuthorizerService, AuthorizerService>();

      // DataBases
      services.AddDbContext<CajuDbContext>(options => options.UseInMemoryDatabase("Caju"));

      // Repositories
      services.AddScoped<IAccountRepository, AccountRepository>();
    }
  }
}
