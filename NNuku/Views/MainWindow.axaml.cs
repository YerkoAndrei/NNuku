using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace NNuku.Views;
using static Constantes;

public partial class MainWindow : Window
{
    private string fecha;

    public MainWindow()
    {
        InitializeComponent();

        Fecha.Text = FormatearFechaLarga(DateTime.Now);
        fecha = FormatearFechaEstándar(DateTime.Now);
    }

    private void CrearNota()
    {
        if (string.IsNullOrEmpty(Nota.Text))
            return;

        var nuevaNota = new Nota
        {
            Fecha = fecha,
            Texto = Nota.Text
        };

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
        this.Close();
    }

    public void EnClicGuardarYSalir(object sender, RoutedEventArgs args)
    {
        CrearNota();
        this.Close();
    }

    public void EnClicNotas(object sender, RoutedEventArgs args)
    {
        CrearNota();
        //App.Current.MainPage = new NavigationPage(new Diario());
    }
}
