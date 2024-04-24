using AuthorisationService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthorisationService.Core.Helper;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Setup DB
        
        modelBuilder.Entity<Authorisation>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Authorisation>().HasIndex(u => u.Email).IsUnique();
        #endregion
    }

    public DbSet<Authorisation> Authorisations { get; set; }
}
