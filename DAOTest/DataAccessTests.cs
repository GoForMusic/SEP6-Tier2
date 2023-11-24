using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using RestServer.Data;
using Shared;
using Xunit;


namespace DAOTest
{
    /// <summary>
    /// Class which will test the db using mocking
    /// </summary>
    public class DataAccessTests
    {
        /// <summary>
        /// Method to test creating an account
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateAccount_ShouldAddAccountToDatabase()
        {
            // Arrange
            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "CreateAccount_ShouldAddAccountToDatabase").Options;

            // Act
            await using var context = new Context(options);
            DataAccess dataAccess = new DataAccess(context);
            Account account = new Account { UserName = "testuser" };
            await dataAccess.CreateAccount(account);

            // Assert
            Assert.Equal(1, context.Accounts.Count());
            Assert.Equal(account.UserName, context.Accounts.Single().UserName);
        }

        /// <summary>
        /// Method to test getting all accounts
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllAccounts_ShouldReturnAllAccounts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "GetAllAccounts_ShouldReturnAllAccounts").Options;

            await using var context = new Context(options);
            var dataAccess = new DataAccess(context);

            // Add into the database test data
            var accounts = new List<Account>
            {
                new Account { UserName = "user1" },
                new Account { UserName = "user2" },
                new Account { UserName = "user3" }
            };
            context.Accounts.AddRange(accounts);
            await context.SaveChangesAsync();

            // Act
            var result = await dataAccess.GetAllAccounts();

            // Assert
            Assert.Equal(accounts.Count, result.Count);
            Assert.Equal(accounts.Select(a => a.UserName), result.Select(a => a.UserName));
        }
        
        /// <summary>
        /// Method to test getting an account by the username
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAccountWithUserName_ShouldReturnCorrectAccount()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "GetAccountWithUserName_ShouldReturnCorrectAccount").Options;

            await using var context = new Context(options);
            var dataAccess = new DataAccess(context);

            // Add into the database test data
            var accounts = new List<Account>
            {
                new Account { UserName = "user1" },
                new Account { UserName = "testuser" }, // Add the account you want to search for
                new Account { UserName = "user3" }
            };
            context.Accounts.AddRange(accounts);
            await context.SaveChangesAsync();

            // Act
            var result = await dataAccess.GetAccountWithUserName("testuser");

            // Assert
            Assert.Single(result);
            Assert.Equal("testuser", result.First().UserName);
        }

        /// <summary>
        /// Method to test getting an account using id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAccountWithId_ShouldReturnCorrectAccount()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "GetAccountWithId_ShouldReturnCorrectAccount").Options;

            await using var context = new Context(options);
            var dataAccess = new DataAccess(context);

            // Add into the database test data
            var accounts = new List<Account>
            {
                new Account { Id = 1, UserName = "testUserName1"},
                new Account { Id = 2, UserName = "testUserName2"},
                new Account { Id = 3, UserName = "testUserName3"}
            };
            context.Accounts.AddRange(accounts);
            await context.SaveChangesAsync();

            // Act
            var result = await dataAccess.GetAccountWithId(2);

            // Assert
            Assert.Single(result);
            Assert.Equal(2, result.First().Id);
        }
    }
}
