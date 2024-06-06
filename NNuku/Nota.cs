namespace NNuku;

public class Nota
{
    public string Fecha { get; set; }
    public string Texto { get; set; }

    public Nota (string fecha, string texto)
    {
        Fecha = fecha;
        Texto = texto;
    }
}