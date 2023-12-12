using Shared;
using Shared.SpecialCases;

namespace RestServer.Data.DAOInterfaces;

public interface IWatchListDAO
{
    /// <summary>
    /// Return a list of movies that a user has it as a watchList
    /// </summary>
    /// <param name="account_id">User PK</param>
    /// <returns>List of watchlist movies</returns>
    public Task<List<WatchList>> GetMoviesWatchListByAccountID(int account_id,int pageNumber,int pageSize);
    /// <summary>
    /// Add a new entry to database for watchlist
    /// </summary>
    /// <param name="addedMovie">The movie FK and User FK stored in db</param>
    public Task<WatchList> AddMovieToWatchList(WatchListREST addedMovie);
    /// <summary>
    /// Remove watchlist entry from db using the movie PK and account pk
    /// </summary>
    /// <param name="removedMovie">The element to be romved</param>
    public Task RemoveMovieFromAWatchList(int watchlistID);
}