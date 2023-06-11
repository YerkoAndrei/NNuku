using System.Globalization;
using Newtonsoft.Json;

namespace NNuku;

public class Constantes
{
    private static string archivoNotas = "diario.ñuku";
    private static string nombreCarpeta = "Ñuku";

    public static bool GuardarNota(Nota nota)
    {
        var carpetaDiario = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nombreCarpeta);
        var archivoDiario = Path.Combine(carpetaDiario, archivoNotas);

        if (!Directory.Exists(carpetaDiario))
            Directory.CreateDirectory(carpetaDiario);

        var notas = CargarNotas();
        notas.Add(nota);

        try
        {
            var json = JsonConvert.SerializeObject(CargarNotas());
            File.WriteAllText(archivoDiario, json);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static List<Nota> CargarNotas()
    {
        var carpetaDiario = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nombreCarpeta);
        var archivoDiario = Path.Combine(carpetaDiario, archivoNotas);

        if (!Directory.Exists(carpetaDiario))
            Directory.CreateDirectory(carpetaDiario);

        if (!File.Exists(archivoDiario))
            return new List<Nota>();

        try
        {
            // Notas guardadas
            var json = File.ReadAllText(archivoDiario);
            var array = JsonConvert.DeserializeObject<Nota[]>(json);

            return array.ToList();
        }
        catch
        {
            return new List<Nota>();
        }
    }

    public static bool ExportarNotas()
    {
        var notas = CargarNotas();

        // Exportar a archivo .txt
        // raiz disco duro android

        return true;
    }

    public static string FormatearFechaLarga(DateTime fecha)
    {
        var día = fecha.ToString("dddd");

        var charArray = día.ToCharArray();
        charArray[0] = char.ToUpper(charArray[0]);
        día = new string(charArray);

        var fechaHora = fecha.ToString("dd/MM/yyyy  hh:mm:ss", CultureInfo.InvariantCulture);
        var tiempo = fecha.ToString("tt", CultureInfo.InvariantCulture).ToLower();

        return día + "  " + fechaHora + " " + tiempo;
    }

    public static string FormatearFechaCorta(DateTime fecha)
    {
        var fechaHora = fecha.ToString("dd/MM/yyyy  hh:mm:ss", CultureInfo.InvariantCulture);
        var tiempo = fecha.ToString("tt", CultureInfo.InvariantCulture).ToLower();

        return fechaHora + " " + tiempo;
    }
}