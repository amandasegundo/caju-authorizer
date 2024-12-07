using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Repositories;
using caju_authorizer_infra.data.Context;

namespace caju_authorizer_infra.data.Repositories
{
  public class MerchantRepository : IMerchantRepository
  {
    public CajuDbContext _context;

    public MerchantRepository(CajuDbContext context)
    {
      _context = context;
    }
    public Merchant GetMerchantByName(string name) => _context.Merchants.FirstOrDefault(e => e.Name == name);
  }
}
