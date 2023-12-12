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
            
        
        return null;
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