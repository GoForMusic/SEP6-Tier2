using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RestServer.Data;

/// <summary>
///  This class set-up factory for the EFC
/// </summary>
public class DbContextFactory : IDesignTimeDbContextFactory<Context>
{
    /// <summary>
    /// A method that create the connection to the database
    /// </summary>
    /// <param name="args"></param>
    /// <returns>The database Context connection</returns>
    public Context CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<Context> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseNpgsql(AppSettings.DATABASE_CONNECTION_STRING);
        return new Context(dbContextOptionsBuilder.Options);
    }
}