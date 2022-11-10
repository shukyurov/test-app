using Microsoft.AspNetCore.Mvc;
using TestApp.App.Accounts;
using TestApp.App.Accounts.Requests;
using TestApp.Domain.Entities;

namespace TestApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAccounts()
        {
            return Ok(_accountService.GetAccounts());
        }

        [HttpPost("create")]
        public ActionResult<IEnumerable<Account>> CreateAccount(CreateAccountRequest accountDto)
        {
            _accountService.CreateAccount(accountDto);

            return Ok();
        }

        [HttpPut("edit")]
        public ActionResult<IEnumerable<Account>> EditAccounts(EditAccountRequest accountDto)
        {
            var result = _accountService.EditAccount(accountDto);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<IEnumerable<Account>> DeleteAccounts(int id)
        {
            var result = _accountService.DeleteAccount(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
