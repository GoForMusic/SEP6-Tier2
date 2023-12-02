using Shared;

namespace RestServer.Data.DAOInterfaces;

public interface IPeopleDAO
{
    
    
    public Task<ICollection<Directors>> GetDirectorsByName(string name);
}