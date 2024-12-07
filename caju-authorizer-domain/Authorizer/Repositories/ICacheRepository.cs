namespace caju_authorizer_domain.Authorizer.Repositories
{
  public interface ICacheRepository
  {
    public string Get(string key);
    public void Set(string key, string value, int expirationInMinutes);
  }
}
