using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSharpFunctionalExtensions;
using Logic.Movies;
using System.Linq.Expressions;
using System.Windows;

namespace UI.Movies;

public partial class MovieListViewModel : ObservableObject
{
    private readonly MovieRepository _repository;

    public IReadOnlyList<Movie> Movies { get; private set; }
    
    public bool ForKidsOnly { get; set; }
    public double MinimumRating { get; set; }
    public bool OnCD { get; set; }

    public MovieListViewModel()
    {
        _repository = new MovieRepository();
        Movies = new List<Movie>().AsReadOnly();
    }

    [RelayCommand]
    private void BuyAdultTicket(long movieId)
    {
        MessageBox.Show("You've bought a ticket", "Success",
            MessageBoxButton.OK, MessageBoxImage.Information);
    }

    [RelayCommand]
    private void BuyChildTicket(long movieId)
    {
        Maybe<Movie> movieOrNothing = _repository.GetOne(movieId);
        if (movieOrNothing.HasNoValue)
            return;

        Movie movie = movieOrNothing.Value;
        var specification = new GenericSpecification<Movie>(Movie.IsSuitableForChildren);
        if (!specification.IsSatisfiedBy(movie))
        {
            MessageBox.Show("The movie is not suitable for children", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBox.Show("You've bought a ticket", "Success",
            MessageBoxButton.OK, MessageBoxImage.Information);
    }

    [RelayCommand]
    private void BuyCD(long movieId)
    {
        Maybe<Movie> movieOrNothing = _repository.GetOne(movieId);
        if (movieOrNothing.HasNoValue)
            return;

        Movie movie = movieOrNothing.Value;
        var specification = new GenericSpecification<Movie>(Movie.HasCDVersion);
        if (!specification.IsSatisfiedBy(movie))
        {
            MessageBox.Show("The movie doesn't have a CD version", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBox.Show("You've bought a ticket", "Success",
            MessageBoxButton.OK, MessageBoxImage.Information);
    }

    [RelayCommand]
    private void Search()
    {
        //_repository.Find()
        //    .Where(x => x.MpaaRating <= MpaaRating.PG || !ForKidsOnly)
        //    .Where(x => x.ReleaseDate <= DateTime.UtcNow.AddMonths(-6) || !OnCD)
        //    .ToList();

        var specification = new GenericSpecification<Movie>(Movie.IsSuitableForChildren);
        Movies = _repository.GetList(specification);
        OnPropertyChanged(nameof(Movies));
    }
}
