using Microsoft.EntityFrameworkCore;
using RestServer.Data;
using Shared;
using Assert = NUnit.Framework.Assert;

namespace UnitTest;

public class UserNUnitTest
{
    private DbContextOptions<Context> _options;
    private Context _context;
    private IDataAccess _dataAccess;

    private async Task InitializeAsync()
    {
        _options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "CreateAccount_ShouldAddAccountToDatabase").Options;
        _context = new Context(_options);
        _dataAccess = new DataAccess(_context);

        // Additional setup if needed
    }

    private async Task CleanupAsync()
    {
        await _context.Database.EnsureDeletedAsync(); // Clear the in-memory database
        _context.Dispose();
        _context = null;
        _dataAccess = null;
    }
    
    [SetUp]
    public void Setup()
    {
        InitializeAsync().GetAwaiter().GetResult();
    }

    [TearDown]
    public void TearDown()
    {
        CleanupAsync().GetAwaiter().GetResult();
    }
    
    [Test]
    public async virtual Task GetUserNotFound()
    {
        var accounts = new List<Account>
        {
            new Account { UserName = "user1" },
            new Account { UserName = "user2" },
            new Account { UserName = "user3" }
        };
        _context.Accounts.AddRange(accounts);
        await _context.SaveChangesAsync();
        
        IEnumerable<Account> getAccount = await _dataAccess.GetAccountWithUserName("abcdergg");
        Assert.IsEmpty(getAccount);
    }

    [Test]
    public async virtual Task GetUserFound()
    {
        var accounts = new List<Account>
        {
            new Account { UserName = "user1" },
            new Account { UserName = "user2" },
            new Account { UserName = "user3" }
        };
        _context.Accounts.AddRange(accounts);
        await _context.SaveChangesAsync();
        
        Assert.NotNull((async () => await _dataAccess.GetAccountWithUserName("user1")));
    }
    
    [Test]
    public async virtual Task ValidateLogin()
    {
        var accounts = new List<Account>
        {
            new Account { UserName = "user1",Password = "123"},
            new Account { UserName = "user2",Password = "123"},
            new Account { UserName = "user3",Password = "123"}
        };

        foreach (var account in accounts)
        {
            await _dataAccess.RegisterAccount(account);
        }
        
        Account local = await _dataAccess.LoginAsync("user1","123");
        Assert.IsNotNull(local);
    }
    
    [Test]
    public async virtual Task InValidateLogin()
    {
        var accounts = new List<Account>
        {
            new Account { UserName = "user1",Password = "123"},
            new Account { UserName = "user2",Password = "123"},
            new Account { UserName = "user3",Password = "123"}
        };
        _context.Accounts.AddRange(accounts);
        await _context.SaveChangesAsync();
        
        Assert.ThrowsAsync<Exception>((async () => await _dataAccess.LoginAsync("x","x")));
    }
}