using caju_authorizer.Filters;
using caju_authorizer_infra.data.Context;
using caju_authorizer_infra.data.Data;
using caju_authorizer_infra.ioc;

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

      InitDataBase.Init(context);

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
