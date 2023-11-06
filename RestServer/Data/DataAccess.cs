using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared;

namespace RestServer.Data
{
    /// <inheritdoc />
    public class DataAccess : IDataAccess
    {

        private readonly Context _context;
        
        /// <summary>
        /// Constructor using injection
        /// </summary>
        /// <param name="context"></param>
        public DataAccess(Context context)
        {
            _context = context;
        }


        /// <summary>
        /// Method to add async an account to db
        /// </summary>
        /// <param name="account">The content of the account; At the moment just id, username, password</param>
        /// <returns></returns>
        public async Task<Account> CreateAccount(Account account)
        {
            EntityEntry<Account> newAccount = await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return newAccount.Entity;
        }

    }
}
