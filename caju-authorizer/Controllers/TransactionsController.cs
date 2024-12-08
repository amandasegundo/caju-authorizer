﻿using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace caju_authorizer_api.Controllers
{
  [ApiController]
  [Route("v1/api/[controller]")]
  public class TransactionsController : ControllerBase
  {
    [HttpPost]
    [Route("Authorize")]
    [ProducesResponseType(typeof(AuthorizerResponse), 200)]
    public IActionResult Post(
    [FromServices] IAuthorizerService authorizerService,
    [FromBody] AuthorizerRequest authorizerRequest
    )
    {
      Stopwatch stopwatch = new();
      stopwatch.Start();

      var response = authorizerService.Authorize(authorizerRequest);

      stopwatch.Stop();
      Console.WriteLine($"Duração da execução: {stopwatch.ElapsedMilliseconds} ms");

      return Ok(response);
    }
  }
}