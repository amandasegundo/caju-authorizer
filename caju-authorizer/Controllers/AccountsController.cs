﻿using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace caju_authorizer_api.Controllers
{
  [ApiController]
  [Route("v1/api/[controller]")]
  public class AccountsController : ControllerBase
  {
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Account>), 200)]
    public IActionResult GetAll(
    [FromServices] IAccountService accountService)
    {
      var response = accountService.GetAccounts();
      return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Account), 200)]
    public IActionResult Get(
    [FromServices] IAccountService accountService,
    [FromRoute] string id )
    {
      var response = accountService.GetAccount(id);
      return Ok(response);
    }
  }
}
