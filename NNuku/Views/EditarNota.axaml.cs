using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace NNuku.Views;
using NNuku.ViewModels;
using static Constantes;

public partial class EditarNota : UserControl
{
    private string fecha = string.Empty;

    public EditarNota()
    {
        InitializeComponent();
        EstablecerFecha();

        Nota.Text = NotaEditando.Texto;
        Loaded += AlCargar;
    }

    private async void AlCargar(object? sender, RoutedEventArgs arg)
    {
        await Task.Delay(100);
        Nota.Focus();
    }

    private void EstablecerFecha()
    {
        Fecha.Text = FormatearFechaLarga(DateTime.Parse(NotaEditando.Fecha));
        fecha = FormatearFechaEstándar(DateTime.Parse(NotaEditando.Fecha));
    }

    public void SobreescribirNota()
    {
        if (string.IsNullOrEmpty(Nota.Text))
            return;

        if (NotaEditando.Texto == Nota.Text)
            return;

        // Guarda nota
        var nuevaNota = new Nota(fecha, Nota.Text);
        if (ActualizarNota(nuevaNota))
            Nota.Text = string.Empty;
    }

    public void EnClicEditar(object sender, RoutedEventArgs args)
    {
        SobreescribirNota();
        MainViewModel.Instancia?.AbirDiarioCommand.Execute(sender);
    }

    public void EnClicCancelar(object sender, RoutedEventArgs args)
    {
        NotaEditando = new Nota(string.Empty, string.Empty);
        MainViewModel.Instancia?.AbirDiarioCommand.Execute(sender);
    }
}
