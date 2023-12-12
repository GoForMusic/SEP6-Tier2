using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RestServer.Data.DAOInterfaces;
using RestServer.Data;
using RestServer.Data.DAOImplementation;
using Shared;
using Shared.SpecialCases;

namespace UnitTest.Tests;

/// <summary>
/// Class where tests comments integration
/// </summary>
public class WatchListTests
{
    // <summary>
    /// Instance of the context
    /// </summary>
    private Context _context;

    /// <summary>
    /// Instance of the interface of IWatchListDAO
    /// </summary>
    private IWatchListDAO _watchListDao;
    
    [SetUp]
    public void Setup()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddScoped<IWatchListDAO, WatchListDAO>();
        services.AddDbContextAsInMemoryDatabase<Context>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        _context = serviceProvider.GetRequiredService<Context>();
        _watchListDao = serviceProvider.GetRequiredService<IWatchListDAO>();
    }
    
    [Test]
    public async Task GetWatchListForAccountCount()
    {
        // Arrange
        await ArrangeDataToDB();
        // Act
        var result = await _watchListDao.GetMoviesWatchListByAccountID(1,1,20);
        var result2 = await _watchListDao.GetMoviesWatchListByAccountID(2,1,20);
        var result3 = await _watchListDao.GetMoviesWatchListByAccountID(3,1,20);
        var result4 = await _watchListDao.GetMoviesWatchListByAccountID(4,1,20);
        
        // Assert
        Assert.AreEqual(4, result.Count);
        Assert.AreEqual(4, result2.Count);
        Assert.AreEqual(4, result3.Count);
        Assert.AreEqual(4, result4.Count);
    }
    
    [Test]
    public async Task DeleteAWatchListMovie()
    {
        // Arrange
        await ArrangeDataToDB();
        
        // Act
        await _watchListDao.RemoveMovieFromAWatchList(4);
        
        // Assert
        Assert.ThrowsAsync<InvalidOperationException>((async () => await _watchListDao.RemoveMovieFromAWatchList(4)));
    }
    
    [Test]
    public async Task TestAddAMovieIfAlreadyExistInDB()
    {
        // Arrange
        await ArrangeDataToDB();
        
        // Act
        Exception exception = Assert.ThrowsAsync<Exception>(async () =>
        {
            await _watchListDao.AddMovieToWatchList(new WatchListREST { account_id = 4, movie_id = 4 });
        });
        // Assert
        Assert.IsNotNull(exception);
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
        var accounts = new List<Account>
        {
            new Account {Id = 1,UserName = "Adrian",Password = "test"},
            new Account {Id = 2,UserName = "Emil",Password = "123"},
            new Account{Id = 3,UserName = "Marian",Password = "Test"},
            new Account {Id = 4,UserName = "Marty",Password = "Martymas"}
        };
        
        _context.Accounts.AddRange(accounts);
        await _context.SaveChangesAsync();
        _context.Movies.AddRange(movies);
        await _context.SaveChangesAsync();
        var movies_from_db = await _context.Movies.ToListAsync();
        var accounts_from_db = await _context.Accounts.ToListAsync();
        
        var watchLists = new List<WatchList>
        {
            new WatchList{account_id = accounts_from_db[0],movie_id = movies_from_db[0]},
            new WatchList{account_id = accounts_from_db[1],movie_id = movies_from_db[0]},
            new WatchList{account_id = accounts_from_db[2],movie_id = movies_from_db[0]},
            new WatchList{account_id = accounts_from_db[3],movie_id = movies_from_db[0]},
            new WatchList{account_id = accounts_from_db[0],movie_id = movies_from_db[1]},
            new WatchList{account_id = accounts_from_db[1],movie_id = movies_from_db[1]},
            new WatchList{account_id = accounts_from_db[2],movie_id = movies_from_db[1]},
            new WatchList{account_id = accounts_from_db[3],movie_id = movies_from_db[1]},
            new WatchList{account_id = accounts_from_db[0],movie_id = movies_from_db[2]},
            new WatchList{account_id = accounts_from_db[1],movie_id = movies_from_db[2]},
            new WatchList{account_id = accounts_from_db[2],movie_id = movies_from_db[2]},
            new WatchList{account_id = accounts_from_db[3],movie_id = movies_from_db[2]},
            new WatchList{account_id = accounts_from_db[0],movie_id = movies_from_db[3]},
            new WatchList{account_id = accounts_from_db[1],movie_id = movies_from_db[3]},
            new WatchList{account_id = accounts_from_db[2],movie_id = movies_from_db[3]},
            new WatchList{account_id = accounts_from_db[3],movie_id = movies_from_db[3]}
        };
        
        _context.WatchLists.AddRange(watchLists);
        await _context.SaveChangesAsync(); 
    }
    
}