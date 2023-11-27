using Microsoft.EntityFrameworkCore;
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


        /// <inheritdoc />
        public async Task<Account> CreateAccount(Account account)
        {
            EntityEntry<Account> newAccount = await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return newAccount.Entity;
        }

        /// <inheritdoc />
        public async Task<ICollection<Account>> GetAllAccounts()
        {
            ICollection<Account> accounts;
            accounts = await _context.Accounts.ToListAsync();
            return accounts;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Account>> GetAccountWithUserName(string username)
        {
            IEnumerable<Account> account;
            ICollection<Account> accounts;
            accounts = await _context.Accounts.ToListAsync();
            account = accounts.Where(t => t.UserName == username);
            return account;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Account>> GetAccountWithId(int id)
        {
            IEnumerable<Account> account;
            ICollection<Account> accounts;
            accounts = await _context.Accounts.ToListAsync();
            account = accounts.Where(t => t.Id == id);
            return account;
        }

        public async Task<Message> GetHelloWorld()
        {
            Message myMessage = new Message();
            myMessage.message = "Hello World";
            return myMessage;
        }

    }
}
