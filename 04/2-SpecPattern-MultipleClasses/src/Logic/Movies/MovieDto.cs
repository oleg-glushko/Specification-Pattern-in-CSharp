namespace Logic.Movies;

public class MovieDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public MpaaRating MpaaRating { get; set; }
    public string Genre { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string Director { get; set; } = string.Empty;
}
