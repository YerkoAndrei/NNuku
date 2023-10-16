namespace NNuku;
using static Constantes;

public partial class Diario : ContentPage
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
        for (int i = 0; i < notas.Count; i++)
        {
            notas[i].Fecha = FormatearFechaCorta(notas[i].Fecha);
        }

        diario.ItemsSource = notas;
    }

    protected override bool OnBackButtonPressed()
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
        return true;
    }

    private void EnClicNuevaNota(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }

    public async void EnClicNota(object sender, SelectedItemChangedEventArgs e)
    {
        var nota = (Nota)e.SelectedItem;
        bool borrar = await DisplayAlert("¿Borrar nota?", nota.Fecha, "Sí", "No");

        if(borrar)
        {
            if (BorrarNota(nota))
            {
                VibrarPositivo();
                CargarDiario();
            }
            else
                VibrarNegativo();
        }
    }

    private void EnClicExportar(object sender, EventArgs e)
    {
        if (ExportarNotas())
            VibrarPositivo();
        else
            VibrarNegativo();
    }
}