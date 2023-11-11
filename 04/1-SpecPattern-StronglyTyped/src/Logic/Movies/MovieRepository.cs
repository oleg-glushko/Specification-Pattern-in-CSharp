using CSharpFunctionalExtensions;
using Logic.Utils;
using System.Linq;
using System.Linq.Expressions;

namespace Logic.Movies;

public class MovieRepository
{
    public Maybe<Movie> GetOne(long id)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Movies.Find(id)!;
    }

    public IReadOnlyList<Movie> GetList(Specification<Movie> specification, double minimumRating,
        int page = 0, int pageSize = 4)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Movies
            .Where(specification.ToExpression())
            .Where(x => x.Rating >= minimumRating)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
    }
}
