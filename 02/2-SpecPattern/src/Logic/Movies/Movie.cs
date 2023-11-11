using CSharpFunctionalExtensions;

namespace Logic.Movies;

public class Movie : Entity
{
    public string Name { get; private set; } = string.Empty;
    public DateTime ReleaseDate { get; private set; }
    public MpaaRating MpaaRating { get; private set; }
    public string Genre { get; private set; } = string.Empty;
    public double Rating { get; private set; }

    public bool IsSuitableForChildren()
    {
        return MpaaRating <= MpaaRating.PG;
    }

    public bool HasCDVersion()
    {
        return ReleaseDate <= DateTime.Now.AddMonths(-6);
    }
}

public enum MpaaRating
{
    G = 1,
    PG = 2,
    PG13 = 3,
    R = 4
}