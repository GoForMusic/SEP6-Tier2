using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RestServer.Data;
using RestServer.Data.DAOImplementation;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace UnitTest.Tests
{
    /// <summary>
    /// Class where tests for search and filter can be found
    /// </summary>
    public class SearchTests
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

        /// <summary>
        /// Test to check if it returns 5 elements
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task SearchMovie_Returns5Elements()
        {
            // Arrange
            string searchTerm = "s";

            var movies = new List<Movie>
            {
                new Movie {Title = "star wars1", Year = 2000},
                new Movie {Title = "star wars2", Year = 2000},
                new Movie {Title = "star wars3", Year = 2000},
                new Movie {Title = "star wars4", Year = 2000},
                new Movie {Title = "star wars5", Year = 2000}
            };
            _context.Movies.AddRange(movies);
            await _context.SaveChangesAsync();

            // Act
            var result = await _movieDAO.SearchMovie(searchTerm);

            var count = result.Count;

            // Assert
            Assert.AreEqual(5, count);
        }

        /// <summary>
        /// Test to check if it returns a list of 21 movies
        /// Checks if the return result is not null
        /// checks if the returned object is an instance of 
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task FilterMoviesByYear_ReturnsMovies()
        {
            // Arrange
            int year = 2000; // Replace with an actual year
            var movies = new List<Movie>
            {
                new Movie {Title = "star wars1", Year = 2000},
                new Movie {Title = "star wars2", Year = 2000},
                new Movie {Title = "star wars3", Year = 2000},
                new Movie {Title = "star wars4", Year = 2000},
                new Movie {Title = "star wars5", Year = 2000},
                new Movie {Title = "star wars6", Year = 2000},
                new Movie {Title = "star wars7", Year = 2000},
                new Movie {Title = "star wars8", Year = 2000},
                new Movie {Title = "star wars9", Year = 2000},
                new Movie {Title = "star wars10", Year = 2000},
                new Movie {Title = "star wars11", Year = 2000},
                new Movie {Title = "star wars12", Year = 2000},
                new Movie {Title = "star wars13", Year = 2000},
                new Movie {Title = "star wars14", Year = 2000},
                new Movie {Title = "star wars15", Year = 2000},
                new Movie {Title = "star wars16", Year = 2000},
                new Movie {Title = "star wars17", Year = 2000},
                new Movie {Title = "star wars18", Year = 2000},
                new Movie {Title = "star wars19", Year = 2000},
                new Movie {Title = "star wars20", Year = 2000},
                new Movie {Title = "star wars21", Year = 2000},
                new Movie {Title = "star wars22", Year = 2000},
                new Movie {Title = "star wars23", Year = 2000},
                new Movie {Title = "star wars24", Year = 2000},
                new Movie {Title = "star wars25", Year = 2000}
            };
            _context.Movies.AddRange(movies);
            await _context.SaveChangesAsync();
            // Act
            var result = await _movieDAO.FilterMoviesByYear(year);
            var count = result.Count;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(21, count);
            Assert.IsInstanceOf<List<Movie>>(result);
            
        }

    }
}
