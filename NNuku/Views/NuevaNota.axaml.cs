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

        Fecha.Text = FormatearFechaLarga(DateTime.Now);
        fecha = FormatearFechaEst�ndar(DateTime.Now);
    }

    private void CrearNota()
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

    private void VolverAtras()
    {
        CrearNota();
        MainViewModel.instancia.CerrarCommand.Execute(null);
    }

    public void EnClicGuardarYSalir(object sender, RoutedEventArgs args)
    {
        CrearNota();
        MainViewModel.instancia.CerrarCommand.Execute(sender);
    }

    public void EnClicNotas(object sender, RoutedEventArgs args)
    {
        CrearNota();
        MainViewModel.instancia.AbirDiarioCommand.Execute(sender);
    }
}