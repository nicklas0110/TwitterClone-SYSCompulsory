using Microsoft.EntityFrameworkCore;
using TweetService.Core.Entities;

namespace TweetService.Core.Helper;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Setup DB
        //Auto generate id
        modelBuilder.Entity<Tweet>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        #endregion
        
    }

    public DbSet<Tweet> Tweets { get; set; }
}