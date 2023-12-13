using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RestServer.Data;
using RestServer.Data.DAOImplementation;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace UnitTest.Tests;

/// <summary>
/// Class where tests stars integration
/// </summary>
public class StarsTests
{
    /// <summary>
    /// Instance of the context
    /// </summary>
    private Context _context;

    /// <summary>
    /// Instance of the interface of IStarsDAO
    /// </summary>
    private IStarsDAO _starsDao;
    
    [SetUp]
    public void Setup()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddScoped<IStarsDAO, StarsDao>();
        services.AddDbContextAsInMemoryDatabase<Context>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        _context = serviceProvider.GetRequiredService<Context>();
        _starsDao = serviceProvider.GetRequiredService<IStarsDAO>();
    }

    [Test]
    public async Task GetStartsForAMovie()
    {
        //Arrange
        await ArrangeDataToDB();
        //Act
        var result1 = await _starsDao.GetStartsFromAMovie(1);
        var result2 = await _starsDao.GetStartsFromAMovie(2);
        var result3 = await _starsDao.GetStartsFromAMovie(3);
        var result4 = await _starsDao.GetStartsFromAMovie(4);
        //Assert
        Assert.AreEqual(4,result1.Count);
        Assert.AreEqual(4,result2.Count);
        Assert.AreEqual(4,result3.Count);
        Assert.AreEqual(4,result4.Count);
    }
    
    private async Task ArrangeDataToDB()
    {
        var movies = new List<Movie>
            {
                new Movie {Id = 1,Title = "star wars1", Year = 2000},
                new Movie {Id = 2,Title = "star wars2", Year = 2000},
                new Movie {Id = 3,Title = "star wars3", Year = 2000},
                new Movie {Id = 4,Title = "star wars4", Year = 2000}
            };
        var people = new List<People>
        {
            new People {Id = 1,Name = "Adrian",BirthYear = 1998},
            new People {Id = 2,Name = "Emil",BirthYear = 1998},
            new People{Id = 3,Name = "Marian",BirthYear = 1998},
            new People {Id = 4,Name = "Marty",BirthYear = 1998}
        };
        
        _context.Peoples.AddRange(people);
        await _context.SaveChangesAsync();
        _context.Movies.AddRange(movies);
        await _context.SaveChangesAsync();
        var movies_from_db = await _context.Movies.ToListAsync();
        var people_from_db = await _context.Peoples.ToListAsync();
        
        var starts = new List<Stars>
        {
            new Stars{movie_id = movies_from_db[0],person_id = people_from_db[0]},
            new Stars{movie_id = movies_from_db[0],person_id = people_from_db[1]},
            new Stars{movie_id = movies_from_db[0],person_id = people_from_db[2]},
            new Stars{movie_id = movies_from_db[0],person_id = people_from_db[3]},
            new Stars{movie_id = movies_from_db[1],person_id = people_from_db[0]},
            new Stars{movie_id = movies_from_db[1],person_id = people_from_db[1]},
            new Stars{movie_id = movies_from_db[1],person_id = people_from_db[2]},
            new Stars{movie_id = movies_from_db[1],person_id = people_from_db[3]},
            new Stars{movie_id = movies_from_db[2],person_id = people_from_db[0]},
            new Stars{movie_id = movies_from_db[2],person_id = people_from_db[1]},
            new Stars{movie_id = movies_from_db[2],person_id = people_from_db[2]},
            new Stars{movie_id = movies_from_db[2],person_id = people_from_db[3]},
            new Stars{movie_id = movies_from_db[3],person_id = people_from_db[0]},
            new Stars{movie_id = movies_from_db[3],person_id = people_from_db[1]},
            new Stars{movie_id = movies_from_db[3],person_id = people_from_db[2]},
            new Stars{movie_id = movies_from_db[3],person_id = people_from_db[3]}
            
        };
        _context.Stars.AddRange(starts);
        await _context.SaveChangesAsync(); 
    }
}