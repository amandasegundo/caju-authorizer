namespace caju_authorizer_domain.Authorizer.Config
{
  public class TransactionConfig
  {
    public static string Section = "Parameters:TransactionConfig";
    public int ExpirationTimeInMinutes { get; set; }
  }
}
