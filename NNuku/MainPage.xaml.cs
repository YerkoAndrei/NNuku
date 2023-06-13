namespace NNuku;

using static Constantes;

public partial class MainPage : ContentPage
{
    private bool guardado;

    private double tamañoFecha = 18;
    private double tamañoTexto = 22;

    public MainPage()
    {
        InitializeComponent();

        fecha.Text = FormatearFechaLarga(DateTime.Now);
        guardado = false;
    }

    protected override bool OnBackButtonPressed()
    {
        // Segundo clic cierra
        if (guardado)
            return true;

        // Primer clic guarda
        guardado = true;
        CrearNota();
        return false;
    }

    private void CrearNota()
    {
        if (nota.Text.Length <= 0)
            return;

        var nuevaNota = new Nota();
        nuevaNota.Fecha = FormatearFechaEstándar(DateTime.Now);
        nuevaNota.Texto = nota.Text;

        // Guarda nota
        if (GuardarNota(nuevaNota))
        {
            VibrarPositivo();
            nota.Text = string.Empty;

            Application.Current.Quit();
        }
        else
            VibrarNegativo();
    }

    private void EnClicGuardarYSalir(object sender, EventArgs e)
    {
        CrearNota();
    }

    private void EnClicVerNotas(object sender, EventArgs e)
    {
        if (nota.Text.Length > 0)
        {
            var nuevaNota = new Nota();
            nuevaNota.Fecha = FormatearFechaEstándar(DateTime.Now);
            nuevaNota.Texto = nota.Text;

            // Guarda nota
            if (GuardarNota(nuevaNota))
            {
                nota.Text = string.Empty;
                VibrarPositivo();
            }
            else
                VibrarNegativo();
        }

        App.Current.MainPage = new NavigationPage(new Diario());
    }

    private void EnClicExportar(object sender, EventArgs e)
    {
        if (ExportarNotas())
            VibrarPositivo();
        else
            VibrarNegativo();
    }
}
