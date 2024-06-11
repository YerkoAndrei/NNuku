using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NNuku.ViewModels;

namespace NNuku.Views;
using static Constantes;

public partial class Diario : UserControl
{
    private Nota notaBorrar;

    public Diario()
    {
        InitializeComponent();
        CargarDiario();
    }

    private void CargarDiario()
    {
        var notas = CargarNotas();
        notas = notas.OrderByDescending(o => o.Fecha).ToList();

        // Formatea fechas
        foreach (var nota in notas)
        {
            nota.Fecha = FormatearFechaCorta(nota.Fecha);
        }

        Notas.ItemsSource = notas;
    }

    public void EnClicNuevaNota(object sender, RoutedEventArgs args)
    {
        MainViewModel.Instancia.AbirNuevaNotaCommand.Execute(sender);
    }

    public void EnClicNota(object sender, SelectionChangedEventArgs args)
    {
        var nota = Notas.SelectedItem as Nota;
        if (nota == null)
            return;

        // Abre popup
        notaBorrar = nota;
        FechaBorrar.Text = nota.Fecha;
        PopupBorrar.IsVisible = true;
        Notas.SelectedIndex = -1;
    }

    public void EnClicSÌBorrar(object sender, RoutedEventArgs args)
    {
        if (BorrarNota(notaBorrar))
        {
            MostrarPositivo();
            CargarDiario();
        }
        else
            MostrarNegativo();

        PopupBorrar.IsVisible = false;
    }

    public void EnClicNoBorrar(object sender, RoutedEventArgs args)
    {
        PopupBorrar.IsVisible = false;
    }

    public void EnClicExportar(object sender, RoutedEventArgs args)
    {
        if (ExportarNotas())
            MostrarPositivo();
        else
            MostrarNegativo();
    }
}
