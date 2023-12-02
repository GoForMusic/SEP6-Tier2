using Microsoft.EntityFrameworkCore;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace RestServer.Data.DAOImplementation;

public class PeopleDAO : IPeopleDAO
{
    private readonly Context _context;

    /// <summary>
    /// Constructor using injection
    /// </summary>
    /// <param name="context"></param>
    public PeopleDAO(Context context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Directors>> GetDirectorsByName(string name)
    {
        List<Directors> directors = await _context.Directors
            .Include(m=>m.movie_id).Include(d => d.person_id) // Ensure the navigation property name is correct
            .Where(d => d.person_id.Name.ToLower().Contains(name.ToLower()))
            .Take(5)
            .ToListAsync();

        return directors;
    }
}