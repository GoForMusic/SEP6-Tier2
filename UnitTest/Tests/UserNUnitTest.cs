using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RestServer.Data;
using RestServer.Data.DAOImplementation;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace UnitTest;


public class UserNUnitTest
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
    
    [Test]
    public async virtual Task GetUserNotFound()
    {
        var accounts = new List<Account>
        {
            new Account { UserName = "user1", Password = "123"},
            new Account { UserName = "user2", Password = "123"},
            new Account { UserName = "user3", Password = "123"}
        };
        _context.Accounts.AddRange(accounts);
        await _context.SaveChangesAsync();
        
        IEnumerable<Account> getAccount = await _accountDao.GetAccountWithUserName("abcdergg");
        Assert.IsEmpty(getAccount);
    }

    [Test]
    public async virtual Task GetUserFound()
    {
        var accounts = new List<Account>
        {
            new Account { UserName = "user1", Password = "123"},
            new Account { UserName = "user2", Password = "123"},
            new Account { UserName = "user3", Password = "123"}
        };
        _context.Accounts.AddRange(accounts);
        await _context.SaveChangesAsync();
        
        Assert.NotNull((async () => await _accountDao.GetAccountWithUserName("user1")));
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
            await _accountDao.RegisterAccount(account);
        }
        
        Account local = await _accountDao.LoginAsync("user1","123");
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
        
        Assert.ThrowsAsync<Exception>((async () => await _accountDao.LoginAsync("x","x")));
    }
}