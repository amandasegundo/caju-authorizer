using caju_authorizer_domain.Authorizer.Entities;
using Microsoft.EntityFrameworkCore;

namespace caju_authorizer_infra.data.Context
{
  public class CajuDbContext : DbContext
  {
    public CajuDbContext(DbContextOptions<CajuDbContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Merchant> Merchants { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
  }
}
