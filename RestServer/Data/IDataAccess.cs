using Shared;

namespace RestServer.Data
{
    /// <summary>
    /// Class where methods to interact with db can be found
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// Method to add async an account to db
        /// </summary>
        /// <param name="account">The content of the account; At the moment just id, username, password</param>
        /// <returns>Created status code and its username</returns>
        public Task<Account> CreateAccount(Account account);

        /// <summary>
        /// Get all the accounts in the database
        /// </summary>
        /// <returns>A collection containing all the accounts</returns>
        public Task<ICollection<Account>> GetAllAccounts();

        /// <summary>
        /// Get an account using username
        /// </summary>
        /// <param name="username">provided username</param>
        /// <returns>the specific account</returns>
        public Task<IEnumerable<Account>> GetAccountWithUserName(string username);

        /// <summary>
        /// Get an account using id
        /// </summary>
        /// <param name="id">provided id</param>
        /// <returns>the specific id</returns>
        public  Task<IEnumerable<Account>> GetAccountWithId(int id);

        public  Task<Message> GetHelloWorld();
    }
}
