using Microsoft.AspNetCore.Mvc;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace RestServer.Controller
{

    public class MovieController : ControllerBase
    {
        /// <summary>
        /// Data access instance
        /// </summary>
        private readonly IMovieDAO _service;

        /// <summary>
        /// Constructor with injection of DataAccess
        /// </summary>
        /// <param name="service"></param>
        public MovieController(IMovieDAO service)
        {
            _service = service;
        }

        /// <summary>
        /// Method to return a list of 21 movies based on year
        /// </summary>
        /// <param name="year">the year</param>
        /// <returns>list of 21 movies based on year</returns>
        [HttpGet]
        [Route("/movies/year/{year}")]
        public async Task<ActionResult<List<Movie>>> FilterMoviesByYear(int year)
        {
            try
            {
                ICollection<Movie> movies = await _service.FilterMoviesByYear(year);
                return Ok(movies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Method to return movies with a given title
        /// If st is given, it will return 5 movie that start with st
        /// if sta is given, it will return 5 movies that start with sta
        /// </summary>
        /// <param name="title">the title of the movie</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/movies/search/{title}")]
        public async Task<ActionResult<List<Movie>>> SearchMovie(string title)
        {
            try
            {
                ICollection<Movie> movies = await _service.SearchMovie(title);
                return Ok(movies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
