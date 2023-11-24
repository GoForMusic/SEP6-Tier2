using Microsoft.AspNetCore.Mvc;
using RestServer.Data;
using Shared;

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
        private readonly DataAccess _service;

        /// <summary>
        /// Constructor with injection of DataAccess
        /// </summary>
        /// <param name="service"></param>
        public AccountController(DataAccess service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/account")]
        public async Task<ActionResult> CreateAccount(Account account)
        {
            try
            {
                Account createAccount = await _service.CreateAccount(account);
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

        [HttpGet]
        [Route("/HelloWorld")]
        public async Task<ActionResult<string>> GetHelloWorld()
        {
            try
            {
                string text = await _service.GetHelloWorld();
                return Ok(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
