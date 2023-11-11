using CSharpFunctionalExtensions;
using Logic.Utils;
using Microsoft.EntityFrameworkCore;
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

    public IReadOnlyList<MovieDto> GetList(Specification<Movie> specification, double minimumRating,
        int page = 0, int pageSize = 20)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Movies
            .Where(specification.ToExpression())
            .Where(x => x.Rating >= minimumRating)
            .Skip(page * pageSize)
            .Take(pageSize)
            .Include(x => x.Director)
            .ToList()
            .Select(x => new MovieDto
            {
                Name = x.Name,
                Director = x.Director.Name,
                Genre = x.Genre,
                Id = x.Id,
                MpaaRating = x.MpaaRating,
                Rating = x.Rating,
                ReleaseDate = x.ReleaseDate
            })
            .ToList();
    }
}
