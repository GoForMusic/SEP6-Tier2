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
        public async Task<List<Movie>> SearchMovie(string title,int pageNumber,int pageSize)
        {
            int recordsToSkip = (pageNumber - 1) * pageSize;
            
            List<Movie> movies = await _context.Movies.Where(t => t.Title.ToLower().StartsWith(title.ToLower()))
                .Skip(recordsToSkip) // Skip the appropriate number of records
                .Take(pageSize)      // Take the specified number of records for the page
                .ToListAsync();
            var t = movies.Count;
            return movies;
        }

        /// <inheritdoc />
        public async Task<List<Movie>> FilterMoviesByYear(int year,int pageNumber,int pageSize)
        {
            // Calculate how many records to skip based on the page number and page size
            int recordsToSkip = (pageNumber - 1) * pageSize;
            
            List<Movie> movies = await _context.Movies.Where(m=>m.Year==year)
                .Skip(recordsToSkip) // Skip the appropriate number of records
                .Take(pageSize)      // Take the specified number of records for the page
                .ToListAsync();
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
    
        /// <inheritdoc />
        public async Task<Movie> GetDataByMovieID(long movieID)
        {
            return await _context.Movies.FirstAsync(m => m.Id == movieID);
        }


        /// <inheritdoc />
        public async Task AddRating(int rateValue, long movieId)
        {

            try
            {
                Ratings theRating = await _context.Ratings.FirstAsync(t => t.movie_id.Id == movieId);

                float sumOfRatings = theRating.RatingValue * theRating.Votes;
                sumOfRatings = sumOfRatings + rateValue;
                float finalRating = sumOfRatings / (theRating.Votes + 1);

                theRating.RatingValue = finalRating;
                theRating.Votes += 1;
                await _context.SaveChangesAsync();

            }
            catch (Exception a)
            {
                Console.WriteLine(a);
                throw;
            }
            
        }
    }
}

