using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace NNuku.Views;
using NNuku.ViewModels;
using static Constantes;

public partial class NuevaNota : UserControl
{
    private string fecha = string.Empty;

    public NuevaNota()
    {
        InitializeComponent();
        EstablecerFecha();

        Loaded += AlCargar;
    }

    private async void AlCargar(object? sender, RoutedEventArgs arg)
    {
        await Task.Delay(100);
        Nota.Focus();
    }

    private void EstablecerFecha()
    {
        Fecha.Text = FormatearFechaLarga(DateTime.Now);
        fecha = FormatearFechaEstándar(DateTime.Now);
    }

    public void CrearNota()
    {
        if (string.IsNullOrEmpty(Nota.Text))
            return;

        // Guarda nota
        var nuevaNota = new Nota(fecha, Nota.Text);
        if (GuardarNota(nuevaNota))
            Nota.Text = string.Empty;
    }

    public void VolverAtras()
    {
        CrearNota();
    }

    public void EnClicGuardar(object sender, RoutedEventArgs args)
    {
        CrearNota();
        EstablecerFecha();
    }

    public void EnClicNotas(object sender, RoutedEventArgs args)
    {
        CrearNota();
        MainViewModel.Instancia?.AbirDiarioCommand.Execute(sender);
    }
}
