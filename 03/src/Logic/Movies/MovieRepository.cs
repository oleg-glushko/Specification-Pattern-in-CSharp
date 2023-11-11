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

    public IReadOnlyList<Movie> GetList(GenericSpecification<Movie> specification)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Movies
            .Where(specification.Expression)
            .ToList();
    }

    public IQueryable<Movie> Find()
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Movies;
    }
}
