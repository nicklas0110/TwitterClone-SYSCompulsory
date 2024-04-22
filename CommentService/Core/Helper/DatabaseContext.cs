using CommentService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Core.Helper;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Setup DB
        //Comment model builder
        //Auto generate id
        modelBuilder.Entity<Comment>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        #endregion

        #region Seeding Data
        //Seeding data
        //Creating 5 comment objects
        var comment1 = new Comment
        {
            Id = 1,
            UserId = 1,
            TweetId = 1,
            Body = "Comment",
            CreatedAt = DateTime.Now,
        };

        var comment2 = new Comment
        {
            Id = 2,
            UserId = 2,
            TweetId = 1,
            Body = "Commmeent 2",
            CreatedAt = DateTime.Now,
        };

        var comment3 = new Comment
        {
            Id = 3,
            UserId = 3,
            TweetId = 1,
            Body = "commment 3",
            CreatedAt = DateTime.Now,
        };

        var comment4 = new Comment
        {
            Id = 4,
            UserId = 4,
            TweetId = 1,
            Body = "commmment 4",
            CreatedAt = DateTime.Now,
        };
        

        modelBuilder.Entity<Comment>().HasData(comment1, comment2, comment3, comment4);

        #endregion
    }

    public DbSet<Comment> Comments { get; set; }
}