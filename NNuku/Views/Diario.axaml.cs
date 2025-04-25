using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace NNuku.Views;
using NNuku.ViewModels;
using static Constantes;

public partial class Diario : UserControl
{
    private Nota notaActual = new Nota(string.Empty, string.Empty);

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
        notaActual = nota;
        FechaNota.Text = nota.Fecha;

        // Adelanto
        if (nota.Texto.Length <= 20)
            TextoCortoNota.Text = nota.Texto;
        else if (nota.Texto.Length > 20)
            TextoCortoNota.Text = nota.Texto.Substring(0, 20) + " …";

        PopupNota.IsVisible = true;
        Notas.SelectedIndex = -1;
    }

    public async void EnClicBorrar(object sender, RoutedEventArgs args)
    {
        BorrarNota(notaActual);
        PopupNota.IsVisible = false;

        // Pequeña espera
        await Task.Delay(100);
        CargarDiario();
    }

    public async void EnClicEditar(object sender, RoutedEventArgs args)
    {
        NotaEditando = notaActual;
        MainViewModel.Instancia?.AbirEditarCommand.Execute(sender);
        await Task.Delay(100);
    }

    public void EnClicCancelar(object sender, RoutedEventArgs args)
    {
        PopupNota.IsVisible = false;
    }

    public void EnClicExportar(object sender, RoutedEventArgs args)
    {
        Exportar?.Invoke();
    }
}
