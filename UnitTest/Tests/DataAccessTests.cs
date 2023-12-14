using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RestServer.Data;
using RestServer.Data.DAOImplementation;
using RestServer.Data.DAOInterfaces;
using Shared;
using Assert = NUnit.Framework.Assert;


namespace UnitTest.Tests
{
    /// <summary>
    /// Class which will test the db using mocking
    /// </summary>
    public class DataAccessTests
    {
        
        private Context? _context;
        private IAccountDao? _accountDao;
    
        [SetUp]
        public void Setup()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<IAccountDao, AccountDao>();
            services.AddDbContextAsInMemoryDatabase<Context>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            _context = serviceProvider.GetRequiredService<Context>();
            _accountDao = serviceProvider.GetRequiredService<IAccountDao>();
        }
        
        /// <summary>
        /// Method to test creating an account
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task CreateAccount_ShouldAddAccountToDatabase()
        {
            Account account = new Account { UserName = "testuser", Password = "123"};
            await _accountDao?.CreateAccount(account)!;

            // Assert
            Assert.That(_accountDao.GetAllAccounts().Result.Count, Is.EqualTo(1));
            Assert.That(_accountDao.GetAllAccounts().Result.Single().UserName, Is.EqualTo(account.UserName));
        }

        /// <summary>
        /// Method to test getting all accounts
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllAccounts_ShouldReturnAllAccounts()
        {
            
            // Add into the database test data
            var accounts = new List<Account>
            {
                new Account { UserName = "user1", Password = "123"},
                new Account { UserName = "user2", Password = "123"},
                new Account { UserName = "user3", Password = "123"}
            };

            foreach (var account in accounts)
            {
                await _accountDao?.CreateAccount(account)!;
            }
            await _context?.SaveChangesAsync()!;

            // Act
            var result = await _accountDao?.GetAllAccounts()!;

            // Assert
            Assert.That(result.Count, Is.EqualTo(accounts.Count));
            Assert.That(result.Select(a => a.UserName), Is.EqualTo(accounts.Select(a => a.UserName)));
        }
        
        /// <summary>
        /// Method to test getting an account by the username
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAccountWithUserName_ShouldReturnCorrectAccount()
        {
            // Add into the database test data
            var accounts = new List<Account>
            {
                new Account { UserName = "user1", Password = "123"},
                new Account { UserName = "testuser", Password = "123"}, // Add the account you want to search for
                new Account { UserName = "user3", Password = "123"}
            };
            foreach (var account in accounts)
            {
                await _accountDao?.CreateAccount(account)!;
            }
            await _context?.SaveChangesAsync()!;

            // Act
            var result = _accountDao?.GetAccountWithUserName("testuser").Result.ToList()!;

            // Assert
            Assert.IsNotEmpty(result);
            Assert.That(result.First().UserName, Is.EqualTo("testuser"));
        }

        /// <summary>
        /// Method to test getting an account using id
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAccountWithId_ShouldReturnCorrectAccount()
        {
            // Add into the database test data
            var accounts = new List<Account>
            {
                new Account { Id = 1, UserName = "testUserName1",Password = "123"},
                new Account { Id = 2, UserName = "testUserName2",Password = "123"},
                new Account { Id = 3, UserName = "testUserName3",Password = "123"}
            };
            foreach (var account in accounts)
            {
                await _accountDao?.CreateAccount(account)!;
            }
            await _context?.SaveChangesAsync()!;

            // Act
            var result = _accountDao?.GetAccountWithId(2).Result.ToList();

            // Assert
            Assert.IsNotEmpty(result!);
            Assert.That(result!.First().Id, Is.EqualTo(2));
        }
    }
}
