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

        public async Task<Message> GetHelloWorld2()
        {
            Random random = new Random();
    
            string[] messages = { "Hello World", "Hola Mundo", "Bonjour le monde", "Hallo Welt" }; // Add your messages here
    
            Message myMessage = new Message();
            myMessage.message = messages[random.Next(0, messages.Length)];
    
            return myMessage;
        }

        public async Task<Account> LoginAsync(string username, string password)
        {
            try
            {
                Account account = await _context.Accounts.FirstAsync(t=>t.UserName==username);
                if (validateHashPassword(password, account.Password)) return account;
            }
            catch (Exception e)
            {
                throw new Exception("Username/password incorrect!");
            }

            return null;
        }

        public async Task<Account> RegisterAccount(Account account)
        {
            try
            {
                account.Password = encryptPassword(account.Password);
                EntityEntry<Account> add = await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();
                return add.Entity;
            }catch (Exception e)
            {
                Console.WriteLine(e+" "+ e.StackTrace); 
            }

            return account;
        }

        private string encryptPassword(string passwordPlaintext)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            return BCrypt.Net.BCrypt.HashPassword(passwordPlaintext, salt);
        }
        
        private bool validateHashPassword(string passwordPlaintext, string passwordHash)
        {
            bool password_verified = false;

            if(null == passwordHash || !passwordHash.StartsWith("$2a$"))
                throw new Exception("Invalid hash provided for comparison");

            password_verified = BCrypt.Net.BCrypt.Verify(passwordPlaintext, passwordHash);

            return password_verified;
        }
        
        private void ValidatePassword(string password)
        {
            if (password.Length <= 3)
            {
                throw new Exception("Password must consist of more than 3 letters");
            }
        
            bool capitalLetter = false;
            bool digitLetter = false;

            foreach (char c in password.ToCharArray())
            {
                if (!capitalLetter){
                    if (Char.IsUpper(c)){
                        capitalLetter =true;
                    }
                }

                if (!digitLetter){
                    if (Char.IsDigit(c)){
                        digitLetter=true;
                    }
                }
            }

            if (!capitalLetter){
                throw new Exception("Password needs to contain at least one uppercase letter");
            }
            if (!digitLetter){
                throw new Exception("Password needs to contain at least one digit");
            }
        }

        
    }
}
