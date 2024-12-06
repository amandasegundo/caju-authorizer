using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace caju_authorizer_api.Controllers
{
  [ApiController]
  [Route("v1/api/[controller]")]
  public class TransactionsController : ControllerBase
  {
    [HttpPost]
    [Route("Authorize")]
    public IActionResult Post(
    [FromServices] IAuthorizerService authorizerService,
    [FromBody] AuthorizerRequest transaction
    )
    {
      var response = authorizerService.Authorize(transaction);
      return Ok(response);
    }
  }
}
