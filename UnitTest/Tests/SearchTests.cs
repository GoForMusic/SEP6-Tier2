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
                new Movie {title = "star wars1", year = 2000},
                new Movie {title = "star wars2", year = 2000},
                new Movie {title = "star wars3", year = 2000},
                new Movie {title = "star wars4", year = 2000},
                new Movie {title = "star wars5", year = 2000}
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
                new Movie {title = "star wars1", year = 2000},
                new Movie {title = "star wars2", year = 2000},
                new Movie {title = "star wars3", year = 2000},
                new Movie {title = "star wars4", year = 2000},
                new Movie {title = "star wars5", year = 2000},
                new Movie {title = "star wars6", year = 2000},
                new Movie {title = "star wars7", year = 2000},
                new Movie {title = "star wars8", year = 2000},
                new Movie {title = "star wars9", year = 2000},
                new Movie {title = "star wars10", year = 2000},
                new Movie {title = "star wars11", year = 2000},
                new Movie {title = "star wars12", year = 2000},
                new Movie {title = "star wars13", year = 2000},
                new Movie {title = "star wars14", year = 2000},
                new Movie {title = "star wars15", year = 2000},
                new Movie {title = "star wars16", year = 2000},
                new Movie {title = "star wars17", year = 2000},
                new Movie {title = "star wars18", year = 2000},
                new Movie {title = "star wars19", year = 2000},
                new Movie {title = "star wars20", year = 2000},
                new Movie {title = "star wars21", year = 2000},
                new Movie {title = "star wars22", year = 2000},
                new Movie {title = "star wars23", year = 2000},
                new Movie {title = "star wars24", year = 2000},
                new Movie {title = "star wars25", year = 2000}
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
