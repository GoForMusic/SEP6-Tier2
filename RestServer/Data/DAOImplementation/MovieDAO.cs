using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace RestServer.Data.DAOImplementation
{
    public class MovieDAO : IMovieDAO
    {

        private readonly Context _context;

        /// <summary>
        /// Constructor using injection
        /// </summary>
        /// <param name="context"></param>
        public MovieDAO(Context context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<List<Movie>> SearchMovie(string title)
        {
            List<Movie> movies = await _context.Movies.Where(t => t.title.ToLower().StartsWith(title.ToLower())).Take(5).ToListAsync();

            return movies;
        }

        /// <inheritdoc />
        public async Task<List<Movie>> FilterMoviesByYear(int year)
        {
            List<Movie> movies = await _context.Movies.Where(m => m.year == year).Take(21).ToListAsync();
            return movies;
        }
    }
}
