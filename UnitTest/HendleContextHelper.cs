using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest;

public static class HendleContextHelper
{
    public static IServiceCollection AddDbContextAsInMemoryDatabase<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        IServiceProvider serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

        return services.AddDbContext<TContext>(
            (sp, options) =>
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseInternalServiceProvider(serviceProvider);
                },ServiceLifetime.Singleton
            );
    }
}