using caju_authorizer_domain.Abstractions.Extensions;
using caju_authorizer_domain.Authorizer.Config;
using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Enums;
using caju_authorizer_domain.Authorizer.Factories;
using caju_authorizer_domain.Authorizer.Handlers.Interfaces;
using caju_authorizer_domain.Authorizer.Helpers;
using caju_authorizer_domain.Authorizer.Repositories;
using caju_authorizer_domain.Authorizer.Resources;
using caju_authorizer_domain.Authorizer.Services.Interfaces;
using Newtonsoft.Json;

namespace caju_authorizer_domain.Authorizer.Services
{
  public class AuthorizerService(
    IAuthorizerHandlerFactory authorizerHandlerFactory,
    IMerchantRepository merchantRepository,
    ITransactionRepository transactionRepository,
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
        Console.Error.WriteLine(AuthorizerResources.MerchantError);
        SaveBadTransaction(authorizerRequest, AuthorizerResources.MerchantError);
        return ErrorResponse();
      }

      var newAuthorizerRequest = new AuthorizerRequest()
      {
        Account = authorizerRequest.Account,
        MCC = merchant.MCC,
        Merchant = merchantName,
        TotalAmount = authorizerRequest.TotalAmount,
      };

      var key = TransactionHelper.GetTransactionCacheKey(newAuthorizerRequest);
      var cacheResponse = cacheRepository.Get(key);

      if (cacheResponse != null)
      {
        Console.Error.WriteLine(AuthorizerResources.DuplicatedError);
        SaveBadTransaction(newAuthorizerRequest, AuthorizerResources.DuplicatedError);
        return ErrorResponse();
      }

      var responseCode = authorizerHandlerFactory.Create(newAuthorizerRequest).Handle();
      cacheRepository.Set(key, JsonConvert.SerializeObject(newAuthorizerRequest), transactionConfig.ExpirationTimeInMinutes);

      SaveSuccessTransaction(newAuthorizerRequest, responseCode, AuthorizerResources.Success);

      Console.Error.WriteLine($"Processamento finalizado");

      return SuccessResponse(responseCode);
    }

    public void SaveBadTransaction(AuthorizerRequest authorizerRequest, string detail)
    {
      var transaction = TransactionFactory.Create(authorizerRequest, ResponseCodes.Error.GetDescription(), detail);
      transactionRepository.InsertTransaction(transaction);
    }

    public void SaveSuccessTransaction(AuthorizerRequest authorizerRequest, string status, string detail)
    {
      var transaction = TransactionFactory.Create(authorizerRequest, status, detail);
      transactionRepository.InsertTransaction(transaction);
    }

    public AuthorizerResponse ErrorResponse()
    {
      return new AuthorizerResponse() { Code = ResponseCodes.Error.GetDescription() };
    }

    public AuthorizerResponse SuccessResponse(string code)
    {
      return new AuthorizerResponse() { Code = code };
    }
  }
}
