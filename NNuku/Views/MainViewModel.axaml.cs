using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;

namespace NNuku.Views;
using static Constantes;

public partial class MainView : UserControl
{
    private string fecha;

    public MainView()
    {
        InitializeComponent();

        Fecha.Text = FormatearFechaLarga(DateTime.Now);
        fecha = FormatearFechaEstándar(DateTime.Now);
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
        (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow?.Close();
    }

    public void EnClicGuardarYSalir(object sender, RoutedEventArgs args)
    {
        CrearNota();
        (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow?.Close();
    }

    public void EnClicNotas(object sender, RoutedEventArgs args)
    {
        CrearNota();
        var diario = new MainView();
        //App.Current.MainPage = new NavigationPage(new Diario());
    }
}
