using Shared;

namespace RestServer.Data.DAOInterfaces
{
    /// <summary>
    /// Interface which deals with accessing the movie table in the db
    /// </summary>
    public interface IMovieDAO
    {
        /// <summary>
        /// Search a movie with given text
        /// If st is given, it will return 5 movie that start with st
        /// if sta is given, it will return 5 movies that start with sta
        /// </summary>
        /// <param name="title">a text to search for in the database</param>
        /// <returns>List of 5 movies</returns>
        public Task<List<Movie>> SearchMovie(string title,int pageNumber,int pageSize);

        /// <summary>
        /// Filter the movies by a given year
        /// </summary>
        /// <param name="year">year to filter by</param>
        /// <returns>A list of 21 movies from the given year( if 21 exists)</returns>
        public Task<List<Movie>> FilterMoviesByYear(int year,int pageNumber,int pageSize);

        /// <summary>
        /// A method that will filter the movies that have a rating between n...n,9
        /// </summary>
        /// <param name="rate">The rate value will be 1...10</param>
        /// <param name="pageNumber">Use for pagination</param>
        /// <param name="pageSize">And default will be 21 if the user will not specify in query</param>
        /// <returns></returns>
        public Task<List<Ratings>> FilterMoviesByRating(int rate,int pageNumber,int pageSize);
        /// <summary>
        /// A method that will get movie details by his ID
        /// </summary>
        /// <param name="movieID">Movie PK</param>
        public Task<Movie> GetDataByMovieID(int movieID);
    }
}
