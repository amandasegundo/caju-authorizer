using caju_authorizer_domain.Abstractions.Extensions;
using caju_authorizer_domain.Authorizer.Config;
using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Enums;
using caju_authorizer_domain.Authorizer.Handlers.Interfaces;
using caju_authorizer_domain.Authorizer.Helpers;
using caju_authorizer_domain.Authorizer.Repositories;
using caju_authorizer_domain.Authorizer.Services.Interfaces;
using Newtonsoft.Json;

namespace caju_authorizer_domain.Authorizer.Services
{
  public class AuthorizerService(
    IAuthorizerHandlerFactory authorizerHandlerFactory,
    IMerchantRepository merchantRepository,
    ICacheRepository cacheRepository,
    TransactionConfig transactionConfig
  ) : IAuthorizerService
  {
    public AuthorizerResponse Authorize(AuthorizerRequest authorizerRequest)
    {
      Console.WriteLine($"Iniciando processamento para {JsonConvert.SerializeObject(authorizerRequest)}");

      var merchantName = MerchantHelper.NormalizeName(authorizerRequest.Merchant);
      var merchant = merchantRepository.GetMerchantByName(merchantName);

      if (merchant is null)
      {
        Console.Error.WriteLine($"O Merchant não foi encontrado na base de dados");
        return ErrorResponse();
      }

      var transaction = new AuthorizerRequest()
      {
        Account = authorizerRequest.Account,
        MCC = merchant.MCC,
        Merchant = merchantName,
        TotalAmount = authorizerRequest.TotalAmount,
      };

      var key = TransactionHelper.GetTransactionCacheKey(transaction);
      var transactionFromCache = cacheRepository.Get(key);

      if (transactionFromCache != null)
      {
        Console.Error.WriteLine($"Transação simultânea detectada, a transação não será processada");
        return ErrorResponse();
      }

      var responseCode = authorizerHandlerFactory.Create(transaction).Handle();
      cacheRepository.Set(key, JsonConvert.SerializeObject(transaction), transactionConfig.ExpirationTimeInMinutes);

      Console.Error.WriteLine($"Processamento finalizado");

      return new AuthorizerResponse()
      {
        Code = responseCode
      };
    }

    public AuthorizerResponse ErrorResponse()
    {
      return new AuthorizerResponse() { Code = ResponseCodes.Error.GetDescription() };
    }
  }
}
