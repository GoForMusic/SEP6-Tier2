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
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Set which db to use
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // UseSqlite or other database provider configuration
                optionsBuilder.UseSqlite("Data Source = movies.db");
            }
        }
        /// <summary>
        /// Constructor for testing
        /// </summary>
        /// <param name="options"></param>
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        /// <summary>
        /// Set up keys and automatic id, required fields, etc 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Account>().HasKey(t => t.Id);
            modelBuilder.Entity<Account>().Property(t => t.Password).IsRequired(false);
        }
    }
}
