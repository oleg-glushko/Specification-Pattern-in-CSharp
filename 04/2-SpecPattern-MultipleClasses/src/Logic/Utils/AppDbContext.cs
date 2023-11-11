using Logic.Movies;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Logic.Utils;

public class AppDbContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
