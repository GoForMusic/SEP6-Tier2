using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RestServer.Data.DAOImplementation;
using RestServer.Data.DAOInterfaces;
using RestServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Microsoft.EntityFrameworkCore;

namespace UnitTest.Tests
{
    public class RatingMovieTests
    {

        /// <summary>
        /// Instance of the context
        /// </summary>
        private Context _context;

        /// <summary>
        /// Instance of the interface of IMovieDAO
        /// </summary>
        private IMovieDAO _movieDAO;

        [SetUp]
        public void Setup()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<IMovieDAO, MovieDAO>();
            services.AddDbContextAsInMemoryDatabase<Context>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            _context = serviceProvider.GetRequiredService<Context>();
            _movieDAO = serviceProvider.GetRequiredService<IMovieDAO>();
        }

        [Test]
        public async Task CheckIfRatingAMovieIsAdded()
        {

            await ArrangeDataToDB();
            _movieDAO.AddRating(5, 1);
            await _context.SaveChangesAsync();


            var updatedRating = await _context.Ratings.FirstAsync(t => t.movie_id.Id == 1);
            Assert.AreEqual(expected: 1, updatedRating.Votes); 
            Assert.AreEqual(expected: 5, updatedRating.RatingValue);

        }


        private async Task ArrangeDataToDB()
        {
            var movies = new List<Movie>
            {
                new Movie {Id = 1,Title = "star wars1", Year = 2000},
                new Movie {Id = 2,Title = "star wars2", Year = 2000},
                new Movie {Id = 3,Title = "star wars3", Year = 2000},
                new Movie {Id = 4,Title = "star wars4", Year = 2000},
                new Movie {Id = 5,Title = "star wars5", Year = 2000}
            };
            
           
            
            _context.Movies.AddRange(movies);
            await _context.SaveChangesAsync();
            var movies_from_db = await _context.Movies.ToListAsync();
           

            var ratings = new List<Ratings>
        {
            new Ratings{movie_id = movies_from_db[0], RatingValue = 0, Votes = 0},
            new Ratings{movie_id = movies_from_db[1], RatingValue = 0, Votes = 0},
            new Ratings{movie_id = movies_from_db[2], RatingValue = 0, Votes = 0},
            new Ratings{movie_id = movies_from_db[3], RatingValue = 0, Votes = 0},
            new Ratings{movie_id = movies_from_db[4], RatingValue = 0, Votes = 0}
        };
            _context.Ratings.AddRange(ratings);
            await _context.SaveChangesAsync();
        }

    }
}
