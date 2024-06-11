using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NNuku.ViewModels;

namespace NNuku.Views;
using static Constantes;

public partial class NuevaNota : UserControl
{
    private string fecha;

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
        {
            MostrarPositivo();
            Nota.Text = string.Empty;
        }
        else
            MostrarNegativo();
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
        MainViewModel.Instancia.AbirDiarioCommand.Execute(sender);
    }
}
