using Microsoft.AspNetCore.Mvc;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace RestServer.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        /// <summary>
        /// Data access instance
        /// </summary>
        private readonly IPeopleDAO _service;

        /// <summary>
        /// Constructor with injection of DataAccess
        /// </summary>
        /// <param name="service"></param>
        public PeopleController(IPeopleDAO service)
        {
            _service = service;
        }


        [HttpGet]
        [Route("/directors/search/{name}")]
        public async Task<ActionResult<ICollection<Directors>>> SearchByDirectorsName(string name)
        {
            try
            {
                ICollection<Directors> movies = await _service.GetDirectorsByName(name);
                return Ok(movies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}