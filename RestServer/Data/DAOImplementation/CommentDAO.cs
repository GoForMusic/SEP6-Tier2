using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestServer.Data.DAOInterfaces;
using Shared;
using Shared.SpecialCases;

namespace RestServer.Data.DAOImplementation;

public class CommentDAO : ICommentDAO
{
    private readonly Context _context;

    public CommentDAO(Context _context)
    {
        this._context = _context;
    }
    
    /// <inheritdoc />
    public async Task<ICollection<Comment>> GetListAsync(int movieID)
    {
        try
        {
            return await _context.Comments
                .Include(c=>c.movie_id)
                .Include(c=>c.WrittenBy)
                .Where(t => t.movie_id.Id == movieID)
                .OrderBy(c=>c.date_posted)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    /// <inheritdoc />
    public async Task<Comment> GetElementAsync(int id)
    {
        try
        {
            //kinda mess????
            return await _context.Comments
                .Include(c=>c.WrittenBy)
                .Include(c=>c.movie_id)
                .FirstAsync(t=>t.Id==id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    /// <inheritdoc />
    public async Task<Comment> AddElementAsync(CommentREST element)
    {
        try
        {
            Comment added = new Comment();
            ConvertToCommentObj(added,element);
            added.WrittenBy = await _context.Accounts.FirstAsync(a=>a.Id==element.account_id);
            added.movie_id = await _context.Movies.FirstAsync(m => m.Id == element.movie_id);
            await _context.Comments.AddAsync(added);
            await _context.SaveChangesAsync();
            return added;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
        
    }

    /// <inheritdoc />
    public async Task DeleteElementAsync(int id)
    {
        try
        {
            Comment? existing = await _context.Comments.FirstAsync(c=>c.Id==id);
            if (existing is null)
            {
                throw new Exception($"Could not find Comment with id {id}. Nothing was deleted");
            }

            _context.Comments.Remove(existing);
            await _context.SaveChangesAsync();
        }catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }

    /// <inheritdoc />
    public async Task UpdateElementAsync(CommentREST element)
    {
        try
        {
            Comment? commentToBeUpdated = await _context.Comments.FirstAsync(c => c.Id == element.Id);
            await ConvertToCommentObj(commentToBeUpdated,element);
            commentToBeUpdated.WrittenBy = await _context.Accounts.FirstAsync(a => a.Id == element.account_id);
            commentToBeUpdated.movie_id = await _context.Movies.FirstAsync(m => m.Id == element.movie_id);
            _context.Comments.Update(commentToBeUpdated);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message + " " + e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }
    /// <inheritdoc />
    public async Task LikeComment(long movieID)
    {
        try
        {
            Comment comment = await _context.Comments.FirstAsync(t => t.Id == movieID);
            if (comment.NumberOfLikes == null)
            {
                comment.NumberOfLikes = 1;
            }
            else
            {
                comment.NumberOfLikes += 1;
            }
            _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UnlikeComment(long movieID)
    {
        try
        {
            Comment comment = await _context.Comments.FirstAsync(t => t.Id == movieID);
            if (comment.NumberOfLikes == null)
            {
                return;
            }
            if (comment.NumberOfLikes == 0)
            {
                return;
            }
            comment.NumberOfLikes -= 1;
            
            _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// A private method that will convert the REST element to DAO element (SecurityISSUEE)
    /// </summary>
    /// <param name="element">CommentREST object</param>
    /// <returns>Comment Object</returns>
    private async Task ConvertToCommentObj(Comment comment,CommentREST element)
    {
        comment.Body = element.Body;
        comment.date_posted = element.date_posted;
    }
    
}