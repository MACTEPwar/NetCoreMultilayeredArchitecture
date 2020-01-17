using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Filters;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AccountDTO account)
        {
            return Ok(_accountService.Create(account));
        }
    }
}