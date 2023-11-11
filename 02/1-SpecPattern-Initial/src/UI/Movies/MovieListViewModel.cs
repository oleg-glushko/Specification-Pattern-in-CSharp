using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logic.Movies;
using System.Windows;

namespace UI.Movies;

public partial class MovieListViewModel : ObservableObject
{
    private readonly MovieRepository _repository;
    public IReadOnlyList<Movie> Movies { get; private set; }

    public MovieListViewModel()
    {
        _repository = new MovieRepository();
        Movies = new List<Movie>().AsReadOnly();
    }

    [RelayCommand]
    private void BuyTicket(long movieId)
    {
        MessageBox.Show("You've bought a ticket", "Success",
            MessageBoxButton.OK, MessageBoxImage.Information);
    }

    [RelayCommand]
    private void Search()
    {
        Movies = _repository.GetList();
        OnPropertyChanged(nameof(Movies));
    }
}
