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
            optionsBuilder.UseSqlite("Data Source = movies.db");
        }

        /// <summary>
        /// Set up keys and automatic id 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Account>().HasKey(t => t.Id);
            //modelBuilder.Entity<Player>().Property(s => s.ID).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Player>().HasKey(t => t.ID);
            //modelBuilder.Entity<HoleScore>().Property(s => s.ID).ValueGeneratedOnAdd();
            //modelBuilder.Entity<HoleScore>().HasKey(t => t.ID);
        }
    }
}
