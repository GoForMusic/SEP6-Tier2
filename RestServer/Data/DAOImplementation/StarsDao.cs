using Microsoft.EntityFrameworkCore;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace RestServer.Data.DAOImplementation;

public class StarsDao : IStarsDAO
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
        return _context.Stars
            .Include(s => s.movie_id)
            .Include(s=>s.person_id)
            .Where(s => s.movie_id.Id == movieid)
            .ToListAsync();
    }
}