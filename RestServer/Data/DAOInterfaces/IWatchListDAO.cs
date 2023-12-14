using Shared;
using Shared.SpecialCases;

namespace RestServer.Data.DAOInterfaces;

public interface IWatchListDao
{
    /// <summary>
    /// Return a list of movies that a user has it as a watchList
    /// </summary>
    /// <param name="accountId">User PK</param>
    ///<param name="pageNumber">default 1 used for pagination</param>
    /// <param name="pageSize">default 21 used for pagination</param>
    /// <returns>List of watchlist movies</returns>
    public Task<List<WatchList>> GetMoviesWatchListByAccountId(int accountId,int pageNumber,int pageSize);
    /// <summary>
    /// Add a new entry to database for watchlist
    /// </summary>
    /// <param name="addedMovie">The movie FK and User FK stored in db</param>
    public Task<WatchList> AddMovieToWatchList(WatchListREST addedMovie);

    /// <summary>
    /// Remove watchlist entry from db using the movie PK and account pk
    /// <param name="watchlistId">WatchList Key</param>
    /// </summary>
    public Task RemoveMovieFromAWatchList(int watchlistId);
}