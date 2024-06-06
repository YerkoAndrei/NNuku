using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NNuku.ViewModels;

namespace NNuku.Views;
using static Constantes;

public partial class Diario : UserControl
{
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
    
    private void VolverAtras()
    {
        MainViewModel.instancia.AbirNuevaNotaCommand.Execute(null);
    }

    public void EnClicNuevaNota(object sender, RoutedEventArgs args)
    {
        MainViewModel.instancia.AbirNuevaNotaCommand.Execute(sender);
    }
    /*
    public async void EnClicNota(object sender, SelectedItemChangedEventArgs e)
    {
        var nota = (Nota)e.SelectedItem;
        bool borrar = await DisplayAlert("¿Borrar nota?", nota.Fecha, "Sí", "No");

        if (borrar)
        {
            if (BorrarNota(nota))
            {
                MostrarPositivo();
                CargarDiario();
            }
            else
                MostrarNegativo();
        }
    }*/

    private void EnClicExportar(object sender, RoutedEventArgs args)
    {
        if (ExportarNotas())
            MostrarPositivo();
        else
            MostrarNegativo();
    }
}
