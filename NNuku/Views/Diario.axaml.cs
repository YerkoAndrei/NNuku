using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace NNuku.Views;
using NNuku.ViewModels;
using static Constantes;

public partial class Diario : UserControl
{
    private Nota notaBorrar = new Nota(string.Empty, string.Empty);

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
        MainViewModel.Instancia?.AbirNuevaNotaCommand.Execute(sender);
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

    public async void EnClicSíBorrar(object sender, RoutedEventArgs args)
    {
        BorrarNota(notaBorrar);
        PopupBorrar.IsVisible = false;

        // Pequeña espera
        await System.Threading.Tasks.Task.Delay(100);
        CargarDiario();
    }

    public void EnClicNoBorrar(object sender, RoutedEventArgs args)
    {
        PopupBorrar.IsVisible = false;
    }

    public void EnClicExportar(object sender, RoutedEventArgs args)
    {
        Exportar?.Invoke();
    }
}
