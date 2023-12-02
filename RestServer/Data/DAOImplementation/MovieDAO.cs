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
            List<Movie> movies = await _context.Movies.Where(t => t.Title.ToLower().StartsWith(title.ToLower())).Take(5).ToListAsync();
            var t = movies.Count;
            return movies;
        }

        /// <inheritdoc />
        public async Task<List<Movie>> FilterMoviesByYear(int year)
        {
            List<Movie> movies = await _context.Movies.Where(m=>m.Year==year).Take(21).ToListAsync();
            return movies;
        }

        /// <inheritdoc />
        public async Task<List<Ratings>> FilterMoviesByRating(int rate,int pageNumber,int pageSize)
        {
            float upperBound = rate + 1; // Calculate upper bound based on lower bound

            // Calculate how many records to skip based on the page number and page size
            int recordsToSkip = (pageNumber - 1) * pageSize;
            
            List<Ratings> ratingsList = await _context.Ratings
                .Where(r => r.RatingValue >= rate && r.RatingValue < upperBound)
                .Include(r => r.movie_id)
                .OrderByDescending(r => r.RatingValue)
                .Skip(recordsToSkip) // Skip the appropriate number of records
                .Take(pageSize)      // Take the specified number of records for the page
                .ToListAsync();

            return ratingsList;
        }
    }
}
