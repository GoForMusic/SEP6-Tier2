using Microsoft.AspNetCore.Mvc;
using RestServer.Data.DAOInterfaces;
using Shared;
using Shared.SpecialCases;

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
    private readonly IWatchListDao _service;

    /// <summary>
    /// Constructor with injection of DataAccess
    /// </summary>
    /// <param name="service"></param>
    public WatchListController(IWatchListDao service)
    {
        _service = service;
    }

    /// <summary>
    /// Method to return a list of 21 movies based on year
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="pageNumber">Use for pagination</param>
    /// <param name="pageSize">Default it will be 21 as empty query otherwise user can use is own</param>
    /// <returns>list of 21 movies based on year</returns>
    [HttpGet]
    [Route("{accountId}")]
    public async Task<ActionResult<ICollection<WatchList>>> GetMoviesFromWatchListByAccountId(int accountId,[FromQuery] int pageNumber,[FromQuery] int pageSize)
    {
        try
        {
            ICollection<WatchList> movies = await _service.GetMoviesWatchListByAccountId(accountId,pageNumber==0?1:pageNumber,pageSize==0?21:pageSize);
            return Ok(movies);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult> AddMovieToWatchList([FromBody] WatchListREST watchList)
    {
        try
        {
            WatchList element = await _service.AddMovieToWatchList(watchList);
            return Ok(element);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("{watchListID}")]
    public async Task<ActionResult> DeleteMovieFromWatchList(int watchListId)
    {
        try
        {
            await _service.RemoveMovieFromAWatchList(watchListId);
            return Ok("Movie removed from watch list.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}