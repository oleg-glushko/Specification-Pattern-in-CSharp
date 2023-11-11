using CSharpFunctionalExtensions;
using System.Linq.Expressions;

namespace Logic.Movies;

public class Movie : Entity
{
    public static readonly Expression<Func<Movie, bool>> IsSuitableForChildren =
        x => x.MpaaRating <= MpaaRating.PG;

    public static readonly Expression<Func<Movie, bool>> HasCDVersion =
        x=> x.ReleaseDate <= DateTime.Now.AddMonths(-6);

    public string Name { get; private set; } = string.Empty;
    public DateTime ReleaseDate { get; private set; }
    public MpaaRating MpaaRating { get; private set; }
    public string Genre { get; private set; } = string.Empty;
    public double Rating { get; private set; }
}

public enum MpaaRating
{
    G = 1,
    PG = 2,
    PG13 = 3,
    R = 4
}