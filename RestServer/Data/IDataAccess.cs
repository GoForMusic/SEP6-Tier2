using Shared;

namespace RestServer.Data
{
    /// <summary>
    /// Class where methods to interact with db can be found
    /// </summary>
    public interface IDataAccess
    {
        public Task<Account> CreateAccount(Account account);
    }
}
