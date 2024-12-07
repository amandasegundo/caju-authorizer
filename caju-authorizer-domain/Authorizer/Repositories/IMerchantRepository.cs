using caju_authorizer_domain.Authorizer.Entities;

namespace caju_authorizer_domain.Authorizer.Repositories
{
  public interface IMerchantRepository
  {
    public Merchant GetMerchantByName(string name);
  }
}
