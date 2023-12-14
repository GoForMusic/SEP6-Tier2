using Shared;

namespace RestServer.Data.DAOInterfaces;

public interface IStarsDao
{
    /// <summary>
    /// A functions that will get a list of Starts for a movie
    /// </summary>
    /// <param name="movieid">Movie PK</param>
    public Task<List<Stars>> GetStartsFromAMovie(long movieid);

    /// <summary>
    /// A method that will the avg rating of all movies that an actor was playing in
    /// </summary>
    /// <param name="actorId">Actor PK</param>
    /// <returns></returns>
    public Task<List<Ratings>> GetDirectorsByName(long actorId);
}