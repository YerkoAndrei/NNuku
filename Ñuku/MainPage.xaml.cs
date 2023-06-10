namespace Ñuku;
using static Constantes;

public partial class MainPage : ContentPage
{
    private bool guardado;
    private bool mostrandoNotas;

    public MainPage()
    {
        InitializeComponent();

        fecha.Text = FormatearFechaLarga(DateTime.Now);
        guardado = false;
    }

    protected override bool OnBackButtonPressed()
    {
        //if(mostrandoNotas)

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
        nuevaNota.Fecha = DateTime.Now;
        nuevaNota.Texto = nota.Text;

        // Guarda nota
        if (GuardarNota(nuevaNota))
        {
            // mensaje correcto
            Application.Current.Quit();
        }
        else
        {
            // mensaje error
        }
    }
}
