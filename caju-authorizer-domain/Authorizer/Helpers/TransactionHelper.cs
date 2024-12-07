using caju_authorizer_domain.Authorizer.Dtos;

namespace caju_authorizer_domain.Authorizer.Helpers
{
  public static class TransactionHelper
  {
    public static string GetTransactionCacheKey(AuthorizerRequest authorizerRequest){
      if (authorizerRequest == null) return null;
      return $"{authorizerRequest.Account}_{authorizerRequest.MCC}_{authorizerRequest.Merchant}_{authorizerRequest.TotalAmount}";
    }
  }
}
