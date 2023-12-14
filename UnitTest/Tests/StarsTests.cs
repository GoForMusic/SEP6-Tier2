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
    private Context? _context;

    /// <summary>
    /// Instance of the interface of IStarsDAO
    /// </summary>
    private IStarsDao? _starsDao;
    
    [SetUp]
    public void Setup()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddScoped<IStarsDao, StarsDao>();
        services.AddDbContextAsInMemoryDatabase<Context>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        _context = serviceProvider.GetRequiredService<Context>();
        _starsDao = serviceProvider.GetRequiredService<IStarsDao>();
    }

    [Test]
    public async Task GetStartsForAMovie()
    {
        //Arrange
        await ArrangeDataToDb();
        //Act
        var result1 = await _starsDao?.GetStartsFromAMovie(1)!;
        var result2 = await _starsDao?.GetStartsFromAMovie(2)!;
        var result3 = await _starsDao?.GetStartsFromAMovie(3)!;
        var result4 = await _starsDao?.GetStartsFromAMovie(4)!;
        //Assert
        Assert.That(result1.Count, Is.EqualTo(4));
        Assert.That(result2.Count, Is.EqualTo(4));
        Assert.That(result3.Count, Is.EqualTo(4));
        Assert.That(result4.Count, Is.EqualTo(4));
    }
    
    [Test]
    public async Task GetStartNameFromStarts()
    {
        //Arrange
        await ArrangeDataToDb();
        //Act
        var result1 = await _starsDao?.GetStartsFromAMovie(1)!;
        //Assert
        Assert.That(result1[0].person_id.Name, Is.EqualTo("Adrian"));
    }
    
    
    
    private async Task ArrangeDataToDb()
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
        
        _context?.Peoples!.AddRange(people);
        await _context?.SaveChangesAsync()!;
        _context.Movies!.AddRange(movies);
        await _context.SaveChangesAsync();
        var moviesFromDb = await _context.Movies.ToListAsync();
        var peopleFromDb = await _context.Peoples!.ToListAsync();
        
        var starts = new List<Stars>
        {
            new Stars{movie_id = moviesFromDb[0],person_id = peopleFromDb[0]},
            new Stars{movie_id = moviesFromDb[0],person_id = peopleFromDb[1]},
            new Stars{movie_id = moviesFromDb[0],person_id = peopleFromDb[2]},
            new Stars{movie_id = moviesFromDb[0],person_id = peopleFromDb[3]},
            new Stars{movie_id = moviesFromDb[1],person_id = peopleFromDb[0]},
            new Stars{movie_id = moviesFromDb[1],person_id = peopleFromDb[1]},
            new Stars{movie_id = moviesFromDb[1],person_id = peopleFromDb[2]},
            new Stars{movie_id = moviesFromDb[1],person_id = peopleFromDb[3]},
            new Stars{movie_id = moviesFromDb[2],person_id = peopleFromDb[0]},
            new Stars{movie_id = moviesFromDb[2],person_id = peopleFromDb[1]},
            new Stars{movie_id = moviesFromDb[2],person_id = peopleFromDb[2]},
            new Stars{movie_id = moviesFromDb[2],person_id = peopleFromDb[3]},
            new Stars{movie_id = moviesFromDb[3],person_id = peopleFromDb[0]},
            new Stars{movie_id = moviesFromDb[3],person_id = peopleFromDb[1]},
            new Stars{movie_id = moviesFromDb[3],person_id = peopleFromDb[2]},
            new Stars{movie_id = moviesFromDb[3],person_id = peopleFromDb[3]}
            
        };
        _context.Stars!.AddRange(starts);
        await _context.SaveChangesAsync(); 
    }
}