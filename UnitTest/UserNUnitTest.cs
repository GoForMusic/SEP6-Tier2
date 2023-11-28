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
    
    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "CreateAccount_ShouldAddAccountToDatabase").Options;
        _context = new Context(_options);
        _dataAccess = new DataAccess(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _options = null;
        _context = null;
        _dataAccess = null;
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
    
    private string encryptPassword(string passwordPlaintext)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        
        return BCrypt.Net.BCrypt.HashPassword(passwordPlaintext, salt);
    }
    
    [Test]
    public async virtual Task ValidateLogin()
    {
        var accounts = new List<Account>
        {
            new Account { UserName = "user1",Password = encryptPassword("123")},
            new Account { UserName = "user2",Password = encryptPassword("123")},
            new Account { UserName = "user3",Password = encryptPassword("123")}
        };
        _context.Accounts.AddRange(accounts);
        await _context.SaveChangesAsync();
        
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