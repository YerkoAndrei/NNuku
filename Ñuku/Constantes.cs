using System.Globalization;
using Newtonsoft.Json;

namespace Ñuku;

public class Constantes
{
    private static string archivoNotas = "notas.ñuku";
    private static string nombreCarpeta = "Ñuku";

    public static bool GuardarNota(Nota nota)
    {
        var carpetaNotasGuardadas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nombreCarpeta);
        var notasGuardadas = Path.Combine(carpetaNotasGuardadas, archivoNotas);

        if (!Directory.Exists(carpetaNotasGuardadas))
            Directory.CreateDirectory(carpetaNotasGuardadas);

        var notas = CargarNotas();
        notas.Add(nota);

        try
        {
            var json = JsonConvert.SerializeObject(CargarNotas());
            File.WriteAllText(notasGuardadas, json);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static List<Nota> CargarNotas()
    {
        var carpetaNotasGuardadas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nombreCarpeta);
        var notasGuardadas = Path.Combine(carpetaNotasGuardadas, archivoNotas);

        if (!Directory.Exists(carpetaNotasGuardadas))
            Directory.CreateDirectory(carpetaNotasGuardadas);

        if (!File.Exists(notasGuardadas))
            return new List<Nota>();

        try
        {
            // Notas guardadas
            var json = File.ReadAllText(notasGuardadas);
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