using Microsoft.EntityFrameworkCore;

namespace Logic.Utils;

public static class DbContextFactory
{
    private static DbContextOptions<AppDbContext>? _dbContextOptions;

    public static void Init(string connectionString)
    {
        _dbContextOptions = BuildDbContextOptionsFactory(connectionString);
    }

    public static AppDbContext GetDbContext()
    {
        if (_dbContextOptions == null)
            throw new InvalidOperationException("Factory is not initialized, run the Init() method before.");
        return new AppDbContext(_dbContextOptions);
    }

    private static DbContextOptions<AppDbContext> BuildDbContextOptionsFactory(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return optionsBuilder.Options;
    }
}
