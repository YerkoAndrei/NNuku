using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NNuku.ViewModels;
using NNuku.Views;
using static NNuku.Constantes;

public partial class MainViewModel : ObservableObject
{
    public static MainViewModel? Instancia;

    [ObservableProperty]
    private Páginas actual;

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
    private void AbirEditar()
    {
        Vista = new EditarNota();
        Actual = Páginas.editar;
    }

    [RelayCommand]
    private void Guardar()
    {
        (Vista as NuevaNota)?.CrearNota();
    }

    [RelayCommand]
    private void EnClicNota()
    {
        AbirNuevaNota();
    }

    [RelayCommand]
    private void ActualizarNota()
    {
        (Vista as EditarNota)?.SobreescribirNota();
        AbirDiario();
    }
}
