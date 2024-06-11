using System;
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
    }

    public void EstablecerFecha()
    {
        Fecha.Text = FormatearFechaLarga(DateTime.Now);
        fecha = FormatearFechaEstándar(DateTime.Now);
        Nota.SelectAll();
    }

    public void CrearNota()
    {
        if (string.IsNullOrEmpty(Nota.Text))
            return;

        var nuevaNota = new Nota(fecha, Nota.Text);

        // Guarda nota
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
