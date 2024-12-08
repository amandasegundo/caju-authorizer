using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Repositories;
using caju_authorizer_infra.data.Context;
using Microsoft.EntityFrameworkCore;

namespace caju_authorizer_infra.data.Repositories.EntityFramework
{
  public class AccountRepository : IAccountRepository
  {
    public CajuDbContext _context;

    public AccountRepository(CajuDbContext context)
    {
      _context = context;
    }
    public Account GetAccount(string id) => _context.Accounts.FirstOrDefault(e => e.Id == id);

    public void UpdateAccount(string id, Account account)
    {
      if (id != account.Id) throw new InvalidDataException("Diferent Id");
      _context.Entry(account).State = EntityState.Modified;
      _context.SaveChanges();
    }
  }
}
