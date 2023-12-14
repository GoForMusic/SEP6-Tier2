using Microsoft.EntityFrameworkCore;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace RestServer.Data.DAOImplementation;

public class StarsDao : IStarsDao
{
    private readonly Context _context;

    /// <summary>
    /// Constructor using injection
    /// </summary>
    /// <param name="context"></param>
    public StarsDao(Context context)
    {
        _context = context;
    }
    
    /// <inheritdoc />
    public Task<List<Stars>> GetStartsFromAMovie(long movieid)
    {
        return _context.Stars!
            .Include(s => s.movie_id)
            .Include(s=>s.person_id)
            .Where(s => s.movie_id.Id == movieid)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<List<Ratings>> GetDirectorsByName(long actorId)
    {
       List<Stars> stars = await _context.Stars!
            .Include(s => s.movie_id)
            .Include(s => s.person_id)
            .Where(s => s.person_id.Id == actorId)
            .ToListAsync();

       List<Ratings> ratingsList = new List<Ratings>();

       foreach (var variablStar in stars)
       {
           ratingsList.Add(await _context.Ratings!.Include(r=>r.movie_id).FirstAsync(r=>r.movie_id.Id==variablStar.movie_id.Id));
       }
       
       return ratingsList;
    }
}