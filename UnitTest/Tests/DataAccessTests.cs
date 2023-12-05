using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RestServer.Data;
using RestServer.Data.DAOImplementation;
using RestServer.Data.DAOInterfaces;
using Shared;
using UnitTest;
using Assert = NUnit.Framework.Assert;


namespace UnitTest.Tests
{
    /// <summary>
    /// Class which will test the db using mocking
    /// </summary>
    public class DataAccessTests
    {
        
        private Context _context;
        private IAccountDAO _accountDao;
    
        [SetUp]
        public void Setup()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<IAccountDAO, AccountDao>();
            services.AddDbContextAsInMemoryDatabase<Context>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            _context = serviceProvider.GetRequiredService<Context>();
            _accountDao = serviceProvider.GetRequiredService<IAccountDAO>();
        }
        
        /// <summary>
        /// Method to test creating an account
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task CreateAccount_ShouldAddAccountToDatabase()
        {
            Account account = new Account { UserName = "testuser", Password = "123"};
            await _accountDao.CreateAccount(account);

            // Assert
            Assert.AreEqual(1, _accountDao.GetAllAccounts().Result.Count);
            Assert.AreEqual(account.UserName, _accountDao.GetAllAccounts().Result.Single().UserName);
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
                await _accountDao.CreateAccount(account);
            }
            await _context.SaveChangesAsync();

            // Act
            var result = await _accountDao.GetAllAccounts();

            // Assert
            Assert.AreEqual(accounts.Count, result.Count);
            Assert.AreEqual(accounts.Select(a => a.UserName), result.Select(a => a.UserName));
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
                await _accountDao.CreateAccount(account);
            }
            await _context.SaveChangesAsync();

            // Act
            var result = await _accountDao.GetAccountWithUserName("testuser");

            // Assert
            Assert.IsNotEmpty(result);
            Assert.AreEqual("testuser", result.First().UserName);
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
                await _accountDao.CreateAccount(account);
            }
            await _context.SaveChangesAsync();

            // Act
            var result = await _accountDao.GetAccountWithId(2);

            // Assert
            Assert.IsNotEmpty(result);
            Assert.AreEqual(2, result.First().Id);
        }
    }
}
