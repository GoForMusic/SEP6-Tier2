using Microsoft.AspNetCore.Mvc;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace RestServer.Controller;

[ApiController]
[Route("[controller]")]
public class StarsController : ControllerBase
{
    /// <summary>
    /// Data access instance
    /// </summary>
    private readonly IStarsDAO _service;

    /// <summary>
    /// Constructor with injection of DataAccess
    /// </summary>
    /// <param name="service"></param>
    public StarsController(IStarsDAO service)
    {
        _service = service;
    }
    
    /// <summary>
    /// REST Controller to get a list of starts by a movieID
    /// </summary>
    /// <param name="movieId">Movie PK</param>
    [HttpGet]
    [Route("{movieId}")]
    public async Task<ActionResult<List<Stars>>> GetStartsByMovie(long movieId)
    {
        try
        {
            List<Stars> starsList = await _service.GetStartsFromAMovie(movieId);
            return Ok(starsList);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}