using caju_authorizer.Filters;
using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_infra.data.Context;
using caju_authorizer_infra.ioc;
using Microsoft.EntityFrameworkCore;
using System;

namespace caju_authorizer_api
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddSwaggerGen();

      IoC.Configure(services);

      services.AddControllers(options =>
      {
        options.Filters.Add(new CustomExceptionFilter());
      });
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CajuDbContext context)
    {
      if (env.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
      }

      DataBaseIngestion(context);

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    public void DataBaseIngestion(CajuDbContext context)
    {
      context.Accounts.Add(new Account { Id = "101", FoodBalance = 1000, MealBalance = 1000, CashBalance = 1000 });
      context.Accounts.Add(new Account { Id = "102", FoodBalance = 1000, MealBalance = 1000, CashBalance = 1000 });
      context.Accounts.Add(new Account { Id = "103", FoodBalance = 1000, MealBalance = 1000, CashBalance = 1000 });
      context.Accounts.Add(new Account { Id = "104", FoodBalance = 1000, MealBalance = 1000, CashBalance = 1000 });
      context.SaveChanges();
    }
  }
}
