namespace caju_authorizer_domain.Authorizer.Resources
{
  public static class AuthorizerResources
  {
    public const string MerchantError = "O Merchant não foi encontrado na base de dados, a transação não será processada";
    public const string DuplicatedError = "Transação simultânea detectada, a transação não será processada";
    public const string Success = "Transação processada com sucesso";
  }
}
