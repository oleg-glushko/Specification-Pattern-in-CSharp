using System.Windows;
using Logic.Utils;

namespace UI;
public partial class App : Application
{
    public App()
    {
        var connectionString = "Server=(localdb)\\mssqllocaldb;Database=SpecPattern;Trusted_Connection=True;MultipleActiveResultSets=true";
        Initer.Init(connectionString);
    }
}

