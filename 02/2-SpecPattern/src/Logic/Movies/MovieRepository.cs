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

    public IReadOnlyList<Movie> GetList(bool forKidsOnly, double minimumRating, bool availableOnCD)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Movies
            .Where(x =>
                (x.MpaaRating <= MpaaRating.PG || !forKidsOnly) &&
                x.Rating >= minimumRating &&
                (x.ReleaseDate <= DateTime.Now.AddMonths(-6) || !availableOnCD))
            .ToList();
    }
}
