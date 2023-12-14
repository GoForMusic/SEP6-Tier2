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
    private Context? _context;

    /// <summary>
    /// Instance of the interface of IMovieDAO
    /// </summary>
    private ICommentDao? _commentDao;
    
    [SetUp]
    public void Setup()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddScoped<ICommentDao, CommentDao>();
        services.AddDbContextAsInMemoryDatabase<Context>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        _context = serviceProvider.GetRequiredService<Context>();
        _commentDao = serviceProvider.GetRequiredService<ICommentDao>();
    }

    [Test]
    public async Task TestSeeCommentsForAMovie()
    {
        // Arrange
        await ArrangeDataToDb();
        // Act
        var result = await _commentDao?.GetListAsync(1)!;
        var result2 = await _commentDao?.GetListAsync(2)!;
        var result3 = await _commentDao?.GetListAsync(3)!;
        var result4 = await _commentDao?.GetListAsync(4)!;
        
        // Assert
        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result2.Count, Is.EqualTo(4));
        Assert.That(result3.Count, Is.EqualTo(4));
        Assert.That(result4.Count, Is.EqualTo(4));
    }

    [Test]
    public async Task SeeCommentById()
    {
        // Arrange
        await ArrangeDataToDb();
        
        // Act
        //new Comment {Body = "Not wow",movie_id = movies_from_db[0],WrittenBy = accounts_from_db[0]},
        var result = await _commentDao?.GetElementAsync(4)!;
        
        // Assert
        // Check if was writtenBy Adrian for movie 1
        Assert.That(result.WrittenBy.UserName, Is.EqualTo("Adrian"));
        Assert.That(result.movie_id.Title, Is.EqualTo("star wars1"));
    }
    
    [Test]
    public async Task DeleteACommentFromAMovie()
    {
        // Arrange
        await ArrangeDataToDb();
        
        // Act
        await _commentDao?.DeleteElementAsync(4)!;
        
        // Assert
        Assert.ThrowsAsync<InvalidOperationException>((async () => await _commentDao.GetElementAsync(4)));
    }
    
    [Test]
    public async Task UpdateACommentFromAMovie()
    {
        // Arrange
        await ArrangeDataToDb();
        
        // Act

        CommentREST updateComment = new CommentREST
        {
            Id = 4,
            Body = "Update Test Works",
            movie_id = 4,
            account_id = 1
        };
        
        await _commentDao?.UpdateElementAsync(updateComment)!;
        var result = await _commentDao.GetElementAsync(4);
        
        // Assert
        Assert.That(result.Body, Is.EqualTo("Update Test Works"));
    }

    [Test]
    public async Task CheckIfTestIsIncrementing()
    {
        // Arrange
        await ArrangeDataToDb();

        // Act
        await _commentDao?.LikeComment(1)!;

        // Assert
        var comment = await _commentDao.GetElementAsync(1);
        Assert.That(comment.NumberOfLikes, Is.EqualTo(1));
    }

    [Test]
    public async Task CheckIfLikeIsDecrementing()
    {
        //Arrange
        await ArrangeDataToDb();
        await _commentDao?.LikeComment(1)!;
        //Act
        await _commentDao.UnlikeComment(1);
        var comment = await _commentDao.GetElementAsync(1);
        Assert.That(comment.NumberOfLikes, Is.EqualTo(0));
        //Assert
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
        var accounts = new List<Account>
        {
            new Account {Id = 1,UserName = "Adrian",Password = "test"},
            new Account {Id = 2,UserName = "Emil",Password = "123"},
            new Account{Id = 3,UserName = "Marian",Password = "Test"},
            new Account {Id = 4,UserName = "Marty",Password = "Martymas"}
        };
        
        _context?.Accounts!.AddRange(accounts);
        await _context?.SaveChangesAsync()!;
        _context.Movies!.AddRange(movies);
        await _context.SaveChangesAsync();
        var moviesFromDb = await _context.Movies.ToListAsync();
        var accountsFromDb = await _context.Accounts!.ToListAsync();
        
        var comments = new List<Comment>
        {
            new Comment {Body = "Not wow",movie_id = moviesFromDb[0],WrittenBy = accountsFromDb[0]},
            new Comment {Body = "Not wow",movie_id = moviesFromDb[1],WrittenBy = accountsFromDb[0]},
            new Comment {Body = "Not wow",movie_id = moviesFromDb[2],WrittenBy = accountsFromDb[0]},
            new Comment {Body = "Not wow",movie_id = moviesFromDb[3],WrittenBy = accountsFromDb[0]},
            new Comment {Body = "Maybe",movie_id = moviesFromDb[0],WrittenBy = accountsFromDb[1]},
            new Comment {Body = "Maybe",movie_id = moviesFromDb[1],WrittenBy = accountsFromDb[1]},
            new Comment {Body = "Maybe",movie_id = moviesFromDb[2],WrittenBy = accountsFromDb[1]},
            new Comment {Body = "Maybe",movie_id = moviesFromDb[3],WrittenBy = accountsFromDb[1]},
            new Comment {Body = "Amazing",movie_id = moviesFromDb[0],WrittenBy = accountsFromDb[2]},
            new Comment {Body = "Amazing",movie_id = moviesFromDb[1],WrittenBy = accountsFromDb[2]},
            new Comment {Body = "Amazing",movie_id = moviesFromDb[2],WrittenBy = accountsFromDb[2]},
            new Comment {Body = "Amazing",movie_id = moviesFromDb[3],WrittenBy = accountsFromDb[2]},
            new Comment {Body = "Meh",movie_id = moviesFromDb[0],WrittenBy = accountsFromDb[3]},
            new Comment {Body = "Meh",movie_id = moviesFromDb[1],WrittenBy = accountsFromDb[3]},
            new Comment {Body = "Meh",movie_id = moviesFromDb[2],WrittenBy = accountsFromDb[3]},
            new Comment {Body = "Meh",movie_id = moviesFromDb[3],WrittenBy = accountsFromDb[3]},
        };
        _context.Comments!.AddRange(comments);
        await _context.SaveChangesAsync(); 
    }
}