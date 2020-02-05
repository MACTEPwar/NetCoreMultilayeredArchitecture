using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Filters;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService = null;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<ActionResult> Get([FromBody] AccountFilter filter)
        {
            return Ok(_accountService.Read(filter));
        }

        public async Task<ActionResult> Post([FromBody] AccountFilterA filter)
        {
            OperationResult<IList<AccountDTO>> items = null;

            try
            {
                items = _accountService.ReadByFilterAndruha(filter);
                return Ok(items);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


        }
    }
}