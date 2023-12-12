using Microsoft.EntityFrameworkCore;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace RestServer.Data.DAOImplementation;

public class WatchListDAO : IWatchListDAO
{
    private readonly Context _context;

    /// <summary>
    /// Constructor using injection
    /// </summary>
    /// <param name="context"></param>
    public WatchListDAO(Context context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<ICollection<Movie>> GetMoviesWatchListByAccountID(int account_id, int pageNumber, int pageSize)
    {
        int recordsToSkip = (pageNumber - 1) * pageSize;
            
        ICollection<Movie> movies = await _context.WatchLists.Include(w=>w.movie_id)
            .Include(w=>w.account_id)
            .Where(t => t.account_id.Id==account_id)
            .Skip(recordsToSkip) // Skip the appropriate number of records
            .Take(pageSize)      // Take the specified number of records for the page
            .ToListAsync();
        return movies;
    }
    
    /// <inheritdoc />
    public Task AddMovieToWatchList(WatchList addedMovie)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task RemoveMovieFromAWatchList(WatchList removedMovie)
    {
        throw new NotImplementedException();
    }
}