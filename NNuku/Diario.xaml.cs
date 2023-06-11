namespace NNuku;

using static Constantes;

public partial class Diario : ContentPage
{
    private double tamañoFecha = 14;
    private double tamañoTexto = 18;

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

    public async void EnClicNota(object sender, SelectedItemChangedEventArgs e)
    {
        var nota = (Nota)e.SelectedItem;

        bool borrar = await DisplayAlert("¿Borrar nota?", nota.Fecha, "Sí", "No");

        if(borrar)
        {
            if (BorrarNotas(nota))
            {
                CargarDiario();
                // mensaje correcto
            }
            else
            {
                // mensaje error
            }
        }
    }

    private void EnClicNuevaNota(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
}