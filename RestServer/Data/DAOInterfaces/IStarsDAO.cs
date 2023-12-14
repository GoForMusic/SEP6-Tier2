using Shared;

namespace RestServer.Data.DAOInterfaces;

public interface IStarsDao
{
    /// <summary>
    /// A functions that will get a list of Starts for a movie
    /// </summary>
    /// <param name="movieid">Movie PK</param>
    public Task<List<Stars>> GetStartsFromAMovie(long movieid);
}