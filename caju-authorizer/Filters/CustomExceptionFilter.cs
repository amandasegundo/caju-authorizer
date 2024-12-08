using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Enums;
using caju_authorizer_domain.Abstractions.Extensions;

namespace caju_authorizer.Filters
{
  public class CustomExceptionFilter : IExceptionFilter
  {
    public void OnException(ExceptionContext context)
    {
      Console.Error.WriteLine(context.Exception.Message);
      Console.Error.WriteLine(context.Exception.StackTrace);
      context.Result = new OkObjectResult(new AuthorizerResponse
      {
        Code = ResponseCodes.Error.GetDescription()
      });
      context.ExceptionHandled = true;
    }
  }
}