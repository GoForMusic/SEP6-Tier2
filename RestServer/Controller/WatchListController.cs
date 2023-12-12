using Microsoft.AspNetCore.Mvc;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace RestServer.Controller;

/// <summary>
/// Controller for accounts
/// Defined end point can be found here
/// </summary>
[ApiController]
[Route("[controller]")]
public class WatchListController : ControllerBase
{
    /// <summary>
    /// Data access instance
    /// </summary>
    private readonly IWatchListDAO _service;

    /// <summary>
    /// Constructor with injection of DataAccess
    /// </summary>
    /// <param name="service"></param>
    public WatchListController(IWatchListDAO service)
    {
        _service = service;
    }
    
    /// <summary>
    /// Method to return a list of 21 movies based on year
    /// </summary>
    /// <param name="year">the year</param>
    /// <param name="pageNumber">Use for pagination</param>
    /// <param name="pageSize">Default it will be 21 as empty query otherwise user can use is own</param>
    /// <returns>list of 21 movies based on year</returns>
    [HttpGet]
    [Route("/{account_id}")]
    public async Task<ActionResult<ICollection<Movie>>> GetMoviesFromWatchListByAccountID(int account_id,[FromQuery] int pageNumber,[FromQuery] int pageSize)
    {
        try
        {
            ICollection<Movie> movies = await _service.GetMoviesWatchListByAccountID(account_id,pageNumber==0?1:pageNumber,pageSize==0?21:pageSize);
            return Ok(movies);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    [Route("/")]
    public async Task<ActionResult<ICollection<Movie>>> AddMovieToWatchList([FromBody] WatchList watchList)
    {
        try
        {
            await _service.AddMovieToWatchList(watchList);
            return Ok("Movie added successfully to WatchList");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("/")]
    public async Task<ActionResult<ICollection<Movie>>> DeleteMovieFromWatchList([FromBody] WatchList watchList)
    {
        try
        {
            await _service.RemoveMovieFromAWatchList(watchList);
            return Ok("Movie removed from watch list.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}