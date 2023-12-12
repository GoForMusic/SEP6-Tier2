using Microsoft.AspNetCore.Mvc;
using RestServer.Data.DAOInterfaces;
using Shared;
using Shared.SpecialCases;

namespace RestServer.Controller
{
    /// <summary>
    /// Controller for accounts
    /// Defined end point can be found here
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Data access instance
        /// </summary>
        private readonly IAccountDAO _service;

        /// <summary>
        /// Constructor with injection of DataAccess
        /// </summary>
        /// <param name="service"></param>
        public AccountController(IAccountDAO service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/account")]
        public async Task<ActionResult> CreateAccount(Account account)
        {
            try
            {
                Account createAccount = await _service.RegisterAccount(account);
                return Created($"/account/{createAccount.UserName}", account);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get all the accounts in the database
        /// </summary>
        /// <returns>A collection of all the accounts</returns>
        [HttpGet]
        [Route("/accounts")]
        public async Task<ActionResult<ICollection<Account>>> GetAllAccountsAsync()
        {
            try
            {
                ICollection<Account> accounts = await _service.GetAllAccounts();
                return Ok(accounts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        /// <summary>
        /// Get an account by username
        /// </summary>
        /// <param name="username">the username of the account</param>
        /// <returns>the account</returns>
        [HttpGet]
        [Route("/accounts/username")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccountWithUsernameAsync(string username)
        {
            try
            {
                IEnumerable<Account> account = await _service.GetAccountWithUserName(username);
                return Ok(account);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get an account by id
        /// </summary>
        /// <param name="id">the id of the account</param>
        /// <returns>the account</returns>
        [HttpGet]
        [Route("/accounts/id")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccountWithId(int id)
        {
            try
            {
                IEnumerable<Account> account = await _service.GetAccountWithId(id);
                return Ok(account);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get hello world
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/HelloWorld")]
        public async Task<ActionResult<Message>> GetHelloWorld()
        {
            try
            {
                Message text = await _service.GetHelloWorld();
                return Ok(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        /// <summary>
        /// Hello world
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/HelloWorld2")]
        public async Task<ActionResult<Message>> GetHelloWorld2()
        {
            try
            {
                Message text = await _service.GetHelloWorld2();
                return Ok(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        /// <summary>
        /// Log in with userName and password
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        // login user
        public async Task<ActionResult<Account>> LoginAsync([FromBody]RequestLogin requestLogin)
        {
            try
            {
                Account user = await _service.LoginAsync(requestLogin.username,requestLogin.password);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        
       
            // await CacheUserAsync(user!);
        }

        /// <summary>
        /// A path method to update the User password to DB
        /// </summary>
        /// <param name="account">New Account info</param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<ActionResult> AccountPasswordUpdate([FromBody] Account account)
        {
            try
            {
                await _service.AccountPasswordUpdate(account);
                return Ok("Comment updated: " + account.Id);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }  
        }
        
    }
}
