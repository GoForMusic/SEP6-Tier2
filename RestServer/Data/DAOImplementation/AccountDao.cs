using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace RestServer.Data.DAOImplementation
{
    /// <inheritdoc />
    public class AccountDao : IAccountDao
    {

        private readonly Context _context;

        /// <summary>
        /// Constructor using injection
        /// </summary>
        /// <param name="context"></param>
        public AccountDao(Context context)
        {
            _context = context;
        }


        /// <inheritdoc />
        public async Task<Account> CreateAccount(Account account)
        {
            EntityEntry<Account> newAccount = await _context.Accounts!.AddAsync(account);
            await _context.SaveChangesAsync();
            return newAccount.Entity;
        }

        /// <inheritdoc />
        public async Task<ICollection<Account>> GetAllAccounts()
        {
            ICollection<Account> accounts;
            accounts = await _context.Accounts!.ToListAsync();
            return accounts;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Account>> GetAccountWithUserName(string username)
        {
            IEnumerable<Account> account;
            ICollection<Account> accounts;
            accounts = await _context.Accounts!.ToListAsync();
            account = accounts.Where(t => t.UserName == username);
            return account;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Account>> GetAccountWithId(int id)
        {
            IEnumerable<Account> account;
            ICollection<Account> accounts;
            accounts = await _context.Accounts!.ToListAsync();
            account = accounts.Where(t => t.Id == id);
            return account;
        }

        public Task<Message> GetHelloWorld()
        {
            Message myMessage = new Message();
            myMessage.MessageText = "Hello World";
            return Task.FromResult(myMessage);
        }

        public Task<Message> GetHelloWorld2()
        {
            Random random = new Random();

            string[] messages =
                { "Hello World", "Hola Mundo", "Bonjour le monde", "Hallo Welt" }; // Add your messages here

            Message myMessage = new Message();
            myMessage.MessageText = messages[random.Next(0, messages.Length)];

            return Task.FromResult(myMessage);
        }

        public async Task<Account> LoginAsync(string username, string password)
        {
            try
            {
                Account account = await _context.Accounts!.FirstAsync(t => t.UserName == username);
                if (ValidateHashPassword(password, account.Password)) return account;
            }
            catch (Exception)
            {
                throw new Exception("Username/password incorrect!");
            }

            return null!;
        }

        public async Task<Account> RegisterAccount(Account account)
        {
            // Check if the username already exists in the database
            bool isUsernameExists = await _context.Accounts!.AnyAsync(a => a.UserName == account.UserName);

            if (isUsernameExists)
            {
                throw new Exception("Username is already in use.");
            }

            var text = EncryptPassword(account.Password);
            account.Password = text;

            EntityEntry<Account> add = await _context.Accounts!.AddAsync(account);
            await _context.SaveChangesAsync();
            return add.Entity;

        }

        public async Task AccountPasswordUpdate(Account newAccount)
        {
            try
            {
                Account accountToBeUpdated =await _context.Accounts!.FirstAsync(c => c.UserName.Equals(newAccount.UserName));
                var text = EncryptPassword(newAccount.Password);
                accountToBeUpdated.Password = text;
                _context.Accounts!.Update(accountToBeUpdated);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " " + e.StackTrace); // or log to file, etc.
                throw; // re-throw the exception if you want it to continue up the stack
            }
        }

        private string EncryptPassword(string passwordPlaintext)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            return BCrypt.Net.BCrypt.HashPassword(passwordPlaintext, salt);
        }

        private bool ValidateHashPassword(string passwordPlaintext, string passwordHash)
        {
            bool passwordVerified;

            if (null == passwordHash || !passwordHash.StartsWith("$2a$"))
                throw new Exception("Invalid hash provided for comparison");

            passwordVerified = BCrypt.Net.BCrypt.Verify(passwordPlaintext, passwordHash);

            return passwordVerified;
        }
    }
}
