using Shared;

namespace RestServer.Data.DAOInterfaces;

public interface IPeopleDao
{
    
    /// <summary>
    /// A method that will get Directors by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<ICollection<Directors>> GetDirectorsByName(string name);
}