using CSharpFunctionalExtensions;
using Logic.Utils;

namespace Logic.Movies;

public class MovieRepository
{

    public Maybe<Movie> GetOne(long id)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Movies.Find(id);
    }

    public IReadOnlyList<Movie> GetList()
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Movies.ToList();
    }
}
