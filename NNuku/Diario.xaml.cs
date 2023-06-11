namespace NNuku;
using static Constantes;

public partial class Diario : ContentPage
{
	public Diario()
	{
		InitializeComponent();

        var notas = CargarNotas();
        notas = notas.OrderBy(o => o.Fecha).ToList();

        // Formatea fechas
        var dataNotas = new List<DataNota>();
        for (int i=0; i< notas.Count; i++)
        {
            var dataNota = new DataNota();
            dataNota.Fecha = FormatearFechaCorta(notas[i].Fecha);
            dataNota.Texto = notas[i].Texto;
        }

        diario.ItemsSource = dataNotas;
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
}