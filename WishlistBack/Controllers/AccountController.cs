using Microsoft.AspNetCore.Mvc;
using WishlistBack.Abstract;
using WishlistBack.Model;


namespace WishlistBack.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccount AccountServiceDapper;
        public AccountController(IAccount accountServiceDapper)
        {
            AccountServiceDapper = accountServiceDapper ?? throw new ArgumentNullException(nameof(accountServiceDapper));
        }



        [HttpGet, Route("getAllAccounts")]
        public IEnumerable<Account> GetAllAccounts()
        {
            return AccountServiceDapper.GetAllAccounts();
        }


        [HttpGet, Route("getAccountById/{id}")]
        public Account GetAccountById(int id)
        {
            return AccountServiceDapper.GetAccountById(id);
        }

        [HttpPost, Route("createAccount")]
        public Result CreateAccount(Account account)
        {
            return AccountServiceDapper.CreateAccount(account);
        }

        [HttpPut, Route("updateAccount")]
        public Result UpdateAccount(Account account)
        {
            return AccountServiceDapper.UpdateAccount(account);
        }

        [HttpDelete, Route("deleteAccount/{id}")]
        public Result DeleteAccount(int id)
        {
            return AccountServiceDapper.DeleteAccount(id);
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login or password");
            }

            var account = AccountServiceDapper.Login(request.Login, request.Password);

            if (account == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(account); 
        }
    }
}
