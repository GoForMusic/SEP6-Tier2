using System.Collections;
using Microsoft.EntityFrameworkCore;
using RestServer.Data.DAOInterfaces;
using Shared;
using Shared.SpecialCases;

namespace RestServer.Data.DAOImplementation;

public class WatchListDAO : IWatchListDAO
{
    private readonly Context _context;

    /// <summary>
    /// Constructor using injection
    /// </summary>
    /// <param name="context"></param>
    public WatchListDAO(Context context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<List<WatchList>> GetMoviesWatchListByAccountID(int account_id, int pageNumber, int pageSize)
    {
        int recordsToSkip = (pageNumber - 1) * pageSize;
            
        List<WatchList> watchLists= await _context.WatchLists.Include(w=>w.account_id)
            .Include(w=>w.movie_id)
            .Where(w=>w.account_id.Id==account_id)
            .Skip(recordsToSkip) // Skip the appropriate number of records
            .Take(pageSize)      // Take the specified number of records for the page
            .ToListAsync();
        
        return watchLists;
    }
    
    /// <inheritdoc />
    public async Task<WatchList> AddMovieToWatchList(WatchListREST addedMovie)
    {
        try
        {
            //Check if the movie is already there
            var existingEntry = await _context.WatchLists
                .FirstOrDefaultAsync(w => w.account_id.Id == addedMovie.account_id && w.movie_id.Id == addedMovie.movie_id);

            if (existingEntry != null)
            {
                throw new Exception($"Could not add the movie to watchlist because already exist!");
            }
            
            WatchList added = new WatchList();
            added.account_id = await _context.Accounts.FirstAsync(a=>a.Id==addedMovie.account_id);
            added.movie_id = await _context.Movies.FirstAsync(m => m.Id == addedMovie.movie_id);
            await _context.WatchLists.AddAsync(added);
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
    public async Task RemoveMovieFromAWatchList(int watchListId)
    {
        try
        {
            WatchList? existing = await _context.WatchLists.FirstAsync(c=>c.Id==watchListId);
            if (existing is null)
            {
                throw new Exception($"Could not find watchlist with id {watchListId}. Nothing was deleted");
            }

            _context.WatchLists.Remove(existing);
            await _context.SaveChangesAsync();
        }catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+ e.StackTrace); // or log to file, etc.
            throw; // re-throw the exception if you want it to continue up the stack
        }
    }
}