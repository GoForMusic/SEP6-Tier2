using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RestServer.Data.DAOImplementation;
using RestServer.Data.DAOInterfaces;
using RestServer.Data;
using Shared;
using Microsoft.EntityFrameworkCore;

namespace UnitTest.Tests
{
    public class RatingMovieTests
    {

        /// <summary>
        /// Instance of the context
        /// </summary>
        private Context? _context;

        /// <summary>
        /// Instance of the interface of IMovieDAO
        /// </summary>
        private IMovieDao? _movieDao;

        [SetUp]
        public void Setup()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<IMovieDao, MovieDao>();
            services.AddDbContextAsInMemoryDatabase<Context>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            _context = serviceProvider.GetRequiredService<Context>();
            _movieDao = serviceProvider.GetRequiredService<IMovieDao>();
        }

        [Test]
        public async Task CheckIfRatingAMovieIsAdded()
        {

            await ArrangeDataToDb();
            await _movieDao?.AddRating(5, 1)!;
            await _context?.SaveChangesAsync()!;


            var updatedRating = await _context.Ratings!.FirstAsync(t => t.movie_id.Id == 1);
            Assert.That(updatedRating.Votes, Is.EqualTo(expected: 1)); 
            Assert.That(updatedRating.RatingValue, Is.EqualTo(expected: 5));

        }


        private async Task ArrangeDataToDb()
        {
            var movies = new List<Movie>
            {
                new Movie {Id = 1,Title = "star wars1", Year = 2000},
                new Movie {Id = 2,Title = "star wars2", Year = 2000},
                new Movie {Id = 3,Title = "star wars3", Year = 2000},
                new Movie {Id = 4,Title = "star wars4", Year = 2000},
                new Movie {Id = 5,Title = "star wars5", Year = 2000}
            };
            
           
            
            _context?.Movies!.AddRange(movies);
            await _context?.SaveChangesAsync()!;
            var moviesFromDb = await _context.Movies!.ToListAsync();
           

            var ratings = new List<Ratings>
        {
            new Ratings{movie_id = moviesFromDb[0], RatingValue = 0, Votes = 0},
            new Ratings{movie_id = moviesFromDb[1], RatingValue = 0, Votes = 0},
            new Ratings{movie_id = moviesFromDb[2], RatingValue = 0, Votes = 0},
            new Ratings{movie_id = moviesFromDb[3], RatingValue = 0, Votes = 0},
            new Ratings{movie_id = moviesFromDb[4], RatingValue = 0, Votes = 0}
        };
            _context.Ratings!.AddRange(ratings);
            await _context.SaveChangesAsync();
        }

    }
}
