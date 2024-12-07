using caju_authorizer_domain.Abstractions.Extensions;
using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Enums;
using caju_authorizer_domain.Authorizer.Handlers.Interfaces;
using caju_authorizer_domain.Authorizer.Helpers;
using caju_authorizer_domain.Authorizer.Repositories;
using caju_authorizer_domain.Authorizer.Services.Interfaces;

namespace caju_authorizer_domain.Authorizer.Services
{
  public class AuthorizerService(
    IAuthorizerHandlerFactory authorizerHandlerFactory,
    IMerchantRepository merchantRepository
  ) : IAuthorizerService
  {
    public AuthorizerResponse Authorize(AuthorizerRequest authorizerRequest)
    {
      var merchantName = MerchantHelper.NormalizeName(authorizerRequest.Merchant);
      var merchant = merchantRepository.GetMerchantByName(merchantName);

      if(merchant is not null)
      {
        var transaction = new AuthorizerRequest()
        {
          Account = authorizerRequest.Account,
          MCC = merchant.MCC,
          Merchant = merchantName,
          TotalAmount = authorizerRequest.TotalAmount,
        };

        var responseCode = authorizerHandlerFactory.Create(transaction).Handle();
        return new AuthorizerResponse()
        {
          Code = responseCode
        };
      }
      return new AuthorizerResponse()
      {
        Code = ResponseCodes.Error.GetDescription()
      };
    }
  }
}
