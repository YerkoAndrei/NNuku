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
            // mensaje correcto
            //SemanticScreenReader.Announce(CounterBtn.Text);

            nota.Text = string.Empty;
            Application.Current.Quit();
        }
        else
        {
            // mensaje error
        }
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
                // mensaje correcto
                nota.Text = string.Empty;
            }
            else
            {
                // mensaje error
            }
        }

        App.Current.MainPage = new NavigationPage(new Diario());
    }

    private void EnClicExportar(object sender, EventArgs e)
    {
        if (ExportarNotas())
        {
            // mensaje correcto
        }
        else
        {
            // mensaje error
        }
    }
}
