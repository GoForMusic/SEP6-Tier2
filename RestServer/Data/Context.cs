using Microsoft.EntityFrameworkCore;
using Shared;
using System.Numerics;

namespace RestServer.Data
{
    /// <summary>
    /// Class which allows to query and interact with the database
    /// </summary>
    public class Context : DbContext
    {
        
        public DbSet<Movie> Movies { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Directors> Directors { get; set; }
        public DbSet<Stars> Stars { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<Account> Accounts { get; set; }
        
        /// <summary>
        /// Set which db to use
        /// </summary>
        /// <param name="optionsBuilder"></param>

        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Directors>().HasNoKey();
            modelBuilder.Entity<Ratings>().HasNoKey();
            modelBuilder.Entity<Stars>().HasNoKey();
            modelBuilder.Entity<WatchList>().HasNoKey();
        }
    }
}
