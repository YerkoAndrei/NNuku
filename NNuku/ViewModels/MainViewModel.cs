using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNuku.Views;
using static NNuku.Constantes;

namespace NNuku.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public static MainViewModel Instancia;
    private static Páginas Actual;

    [ObservableProperty]
    private UserControl vista;

    public MainViewModel()
    {
        Instancia = this;
        Vista = new NuevaNota();
    }

    [RelayCommand]
    private void AbirNuevaNota()
    {
        Vista = new NuevaNota();
        Actual = Páginas.nuevaNota;
    }

    [RelayCommand]
    private void AbirDiario()
    {
        Vista = new Diario();
        Actual = Páginas.diario;
    }

    [RelayCommand]
    private void Cerrar()
    {
        // Pruebas en escritorio
        var escritorio = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime);
        if (escritorio != null)
            escritorio.MainWindow?.Close();
    }

    [RelayCommand]
    private void EnClicAtras()
    {
        switch (Actual)
        {
            case Páginas.nuevaNota:
                (Vista as NuevaNota)?.VolverAtras();
                break;
            case Páginas.diario:
                (Vista as Diario)?.VolverAtras();
                break;
        }
    }
}
