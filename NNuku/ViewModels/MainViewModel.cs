using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNuku.Views;

namespace NNuku.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public static MainViewModel instancia;

    [ObservableProperty]
    private UserControl vista;

    public MainViewModel()
    {
        instancia = this;
        Vista = new NuevaNota();
    }

    [RelayCommand]
    private void AbirNuevaNota()
    {
        Vista = new NuevaNota();
    }

    [RelayCommand]
    private void AbirDiario()
    {
        Vista = new Diario();
    }

    [RelayCommand]
    private void Cerrar()
    {
        // Pruebas en escritorio
        var escritorio = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime);
        if (escritorio != null)
            escritorio.MainWindow?.Close();
    }
}
