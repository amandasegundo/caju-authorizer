using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_infra.data.Context;

namespace caju_authorizer_infra.data.Data
{
  public static class InitDataBase
  {
    public static void Init(CajuDbContext context)
    {
      // Accounts
      context.Accounts.Add(new Account { Id = "101", FoodBalance = 1000, MealBalance = 1000, CashBalance = 1000 });
      context.Accounts.Add(new Account { Id = "102", FoodBalance = 1000, MealBalance = 1000, CashBalance = 1000 });
      context.Accounts.Add(new Account { Id = "103", FoodBalance = 1000, MealBalance = 1000, CashBalance = 1000 });

      // Merchants
      context.Merchants.Add(new Merchant { Id = "101", Name = "PADARIA DO ZE SAO PAULO BR", MCC = "5411" });
      context.Merchants.Add(new Merchant { Id = "102", Name = "RESTAURANTE SUSHI SAO PAULO BR", MCC = "5412" });
      context.Merchants.Add(new Merchant { Id = "103", Name = "SUPERMERCADO GIASSI BLUMENAU SC", MCC = "5811" });
      context.Merchants.Add(new Merchant { Id = "104", Name = "SUPERMERCADO BISTEK BLUMENAU SC", MCC = "5812" });
      context.Merchants.Add(new Merchant { Id = "105", Name = "KALUNGA BLUMENAU SC", MCC = "1111" });
      context.Merchants.Add(new Merchant { Id = "106", Name = "KABUM JOINVILE SC", MCC = "1111" });

      // Save
      context.SaveChanges();
    }
  }
}
