using caju_authorizer_domain.Authorizer.Config;
using caju_authorizer_domain.Authorizer.Handlers;
using caju_authorizer_domain.Authorizer.Handlers.Interfaces;
using caju_authorizer_domain.Authorizer.Repositories;
using caju_authorizer_domain.Authorizer.Services;
using caju_authorizer_domain.Authorizer.Services.Interfaces;
using caju_authorizer_infra.data.Context;
using caju_authorizer_infra.data.Repositories.Cache;
using caju_authorizer_infra.data.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace caju_authorizer_infra.ioc
{
  public static class IoC
  {
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
      // Configs
      services.AddSingleton(configuration.GetSection(TransactionConfig.Section).Get<TransactionConfig>());

      // Cache
      services.AddMemoryCache();

      // Factories
      services.AddScoped<IAuthorizerHandlerFactory, AuthorizerHandlerFactory>();

      // Services
      services.AddScoped<IAuthorizerService, AuthorizerService>();
      services.AddScoped<IAccountService, AccountService>();

      // DataBases
      services.AddDbContext<CajuDbContext>(options => options.UseInMemoryDatabase("Caju"));

      // Repositories
      services.AddScoped<IAccountRepository, AccountRepository>();
      services.AddScoped<IMerchantRepository, MerchantRepository>();
      services.AddScoped<ICacheRepository, CacheRepository>();
    }
  }
}
