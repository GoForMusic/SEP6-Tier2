using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RestServer.Data;
using RestServer.Data.DAOImplementation;
using RestServer.Data.DAOInterfaces;
using Shared;
using Shared.SpecialCases;

namespace UnitTest.Tests;

/// <summary>
/// Class where tests comments integration
/// </summary>
public class CommentTests
{
    /// <summary>
    /// Instance of the context
    /// </summary>
    private Context _context;

    /// <summary>
    /// Instance of the interface of IMovieDAO
    /// </summary>
    private ICommentDAO _commentDao;
    
    [SetUp]
    public void Setup()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddScoped<ICommentDAO, CommentDAO>();
        services.AddDbContextAsInMemoryDatabase<Context>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        _context = serviceProvider.GetRequiredService<Context>();
        _commentDao = serviceProvider.GetRequiredService<ICommentDAO>();
    }

    [Test]
    public async Task TestSeeCommentsForAMovie()
    {
        // Arrange
        await ArrangeDataToDB();
        // Act
        var result = await _commentDao.GetListAsync(1);
        var result2 = await _commentDao.GetListAsync(2);
        var result3 = await _commentDao.GetListAsync(3);
        var result4 = await _commentDao.GetListAsync(4);
        
        // Assert
        Assert.AreEqual(4, result.Count);
        Assert.AreEqual(4, result2.Count);
        Assert.AreEqual(4, result3.Count);
        Assert.AreEqual(4, result4.Count);
    }

    [Test]
    public async Task SeeCommentById()
    {
        // Arrange
        await ArrangeDataToDB();
        
        // Act
        //new Comment {Body = "Not wow",movie_id = movies_from_db[0],WrittenBy = accounts_from_db[0]},
        var result = await _commentDao.GetElementAsync(4);
        
        // Assert
        // Check if was writtenBy Adrian for movie 1
        Assert.AreEqual("Adrian", result.WrittenBy.UserName);
        Assert.AreEqual("star wars1", result.movie_id.Title);
    }
    
    [Test]
    public async Task DeleteACommentFromAMovie()
    {
        // Arrange
        await ArrangeDataToDB();
        
        // Act
        await _commentDao.DeleteElementAsync(4);
        
        // Assert
        Assert.ThrowsAsync<InvalidOperationException>((async () => await _commentDao.GetElementAsync(4)));
    }
    
    [Test]
    public async Task UpdateACommentFromAMovie()
    {
        // Arrange
        await ArrangeDataToDB();
        
        // Act

        CommentREST updateComment = new CommentREST
        {
            Id = 4,
            Body = "Update Test Works",
            movie_id = 4,
            account_id = 1
        };
        
        await _commentDao.UpdateElementAsync(updateComment);
        var result = await _commentDao.GetElementAsync(4);
        
        // Assert
        Assert.That(result.Body, Is.EqualTo("Update Test Works"));
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
        
        var comments = new List<Comment>
        {
            new Comment {Body = "Not wow",movie_id = movies_from_db[0],WrittenBy = accounts_from_db[0]},
            new Comment {Body = "Not wow",movie_id = movies_from_db[1],WrittenBy = accounts_from_db[0]},
            new Comment {Body = "Not wow",movie_id = movies_from_db[2],WrittenBy = accounts_from_db[0]},
            new Comment {Body = "Not wow",movie_id = movies_from_db[3],WrittenBy = accounts_from_db[0]},
            new Comment {Body = "Maybe",movie_id = movies_from_db[0],WrittenBy = accounts_from_db[1]},
            new Comment {Body = "Maybe",movie_id = movies_from_db[1],WrittenBy = accounts_from_db[1]},
            new Comment {Body = "Maybe",movie_id = movies_from_db[2],WrittenBy = accounts_from_db[1]},
            new Comment {Body = "Maybe",movie_id = movies_from_db[3],WrittenBy = accounts_from_db[1]},
            new Comment {Body = "Amazing",movie_id = movies_from_db[0],WrittenBy = accounts_from_db[2]},
            new Comment {Body = "Amazing",movie_id = movies_from_db[1],WrittenBy = accounts_from_db[2]},
            new Comment {Body = "Amazing",movie_id = movies_from_db[2],WrittenBy = accounts_from_db[2]},
            new Comment {Body = "Amazing",movie_id = movies_from_db[3],WrittenBy = accounts_from_db[2]},
            new Comment {Body = "Meh",movie_id = movies_from_db[0],WrittenBy = accounts_from_db[3]},
            new Comment {Body = "Meh",movie_id = movies_from_db[1],WrittenBy = accounts_from_db[3]},
            new Comment {Body = "Meh",movie_id = movies_from_db[2],WrittenBy = accounts_from_db[3]},
            new Comment {Body = "Meh",movie_id = movies_from_db[3],WrittenBy = accounts_from_db[3]},
        };
        _context.Comments.AddRange(comments);
        await _context.SaveChangesAsync(); 
    }
}