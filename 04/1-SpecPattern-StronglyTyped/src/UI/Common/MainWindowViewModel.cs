using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Windows;
using UI.Movies;

namespace UI.Common;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly MovieListViewModel _movieListViewModel = null!;

    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    public MainWindowViewModel()
    {
        //if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
        //    return;
        
        _movieListViewModel = new MovieListViewModel();
        CurrentViewModel = _movieListViewModel;
    }
}
