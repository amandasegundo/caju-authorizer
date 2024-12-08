using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Entities;

namespace caju_authorizer_domain.Authorizer.Factories
{
  public static class TransactionFactory
  {
    public static Transaction Create(AuthorizerRequest authorizerRequest, string status, string detail)
    {
      return new Transaction()
      {
        Id = Guid.NewGuid(),
        AccountId = authorizerRequest.Account,
        Amount = authorizerRequest.TotalAmount,
        MCC = authorizerRequest.MCC,
        Merchant = authorizerRequest.Merchant,
        Date = DateTime.Now,
        Status = status,
        Detail = detail
      };
    }
  }
}
