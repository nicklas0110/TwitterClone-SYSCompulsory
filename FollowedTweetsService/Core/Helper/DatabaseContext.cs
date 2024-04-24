using Microsoft.EntityFrameworkCore;
using FollowedTweetsService.Core.Entities;

namespace FollowedTweetsService.Core.Helper;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Setup DB
        modelBuilder.Entity<FollowedTweets>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        #endregion
    }

    public DbSet<FollowedTweets> FollowedTweets { get; set; }
}