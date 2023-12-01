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

        public Task<List<Movie>> SearchMovie(string title)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> FilterMoviesByYear(int year)
        {
            var movies = await _context.Movies.Where(m => m.year == year).Take(21).ToListAsync();
            return movies;
        }
    }
}
