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
    }
}
