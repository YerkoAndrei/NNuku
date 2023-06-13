using System.Globalization;
using Newtonsoft.Json;

namespace NNuku;

public class Constantes
{
    private static string ISO8006 = "yyyy-MM-ddThh:mm:ss";

    private static string archivoNotas = "diario.ñuku";
    private static string nombreCarpeta = "Ñuku";

    public static void VibrarPositivo()
    {
        Vibration.Default.Vibrate(TimeSpan.FromSeconds(1));
    }

    public static void VibrarNegativo()
    {
        Vibration.Default.Vibrate(TimeSpan.FromSeconds(3));
    }

    public static bool GuardarNota(Nota nota)
    {
        var carpetaDiario = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nombreCarpeta);
        var archivoDiario = Path.Combine(carpetaDiario, archivoNotas);

        if (!Directory.Exists(carpetaDiario))
            Directory.CreateDirectory(carpetaDiario);

        // Agrega nota
        var notas = CargarNotas();
        notas.Add(nota);

        try
        {
            // Sobreescribe diario
            var json = JsonConvert.SerializeObject(notas);
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

        if (!Directory.Exists(carpetaDiario) || !File.Exists(archivoDiario))
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

    public static bool BorrarNotas(Nota nota)
    {
        var carpetaDiario = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nombreCarpeta);
        var archivoDiario = Path.Combine(carpetaDiario, archivoNotas);

        if (!Directory.Exists(carpetaDiario) || !File.Exists(archivoDiario))
            return false;

        try
        {
            // Notas guardadas
            var json = File.ReadAllText(archivoDiario);
            var lista = JsonConvert.DeserializeObject<Nota[]>(json).ToList();

            // Borra nota
            var fechaEstandar = FormatearFechaEstándar(nota.Fecha);
            var notaEncontrada = lista.Where(o => o.Fecha == fechaEstandar && o.Texto == nota.Texto).FirstOrDefault();

            if (!lista.Remove(notaEncontrada))
                return false;

            // Sobreescribe diario
            var jsonNuevo = JsonConvert.SerializeObject(lista);
            File.WriteAllText(archivoDiario, jsonNuevo);
            return true;
        }
        catch
        {
            return false;
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
        var día = fecha.ToString("dddd", CultureInfo.CurrentCulture);

        var charArray = día.ToCharArray();
        charArray[0] = char.ToUpper(charArray[0]);
        día = new string(charArray);

        var fechaHora = fecha.ToString("dd/MM/yyyy  hh:mm:ss", CultureInfo.InvariantCulture);
        var tiempo = fecha.ToString("tt", CultureInfo.InvariantCulture).ToLower();

        return día + "  " + fechaHora + " " + tiempo;
    }

    public static string FormatearFechaCorta(string fecha)
    {
        var fechaDT = DateTime.Parse(fecha, new CultureInfo("cl"));
        var fechaHora = fechaDT.ToString("dd/MM/yyyy  hh:mm:ss", CultureInfo.InvariantCulture);
        var tiempo = fechaDT.ToString("tt", CultureInfo.InvariantCulture).ToLower();

        return fechaHora + " " + tiempo;
    }

    public static string FormatearFechaEstándar(DateTime fecha)
    {
        var fechaHora = fecha.ToString(ISO8006);
        return fechaHora;
    }

    public static string FormatearFechaEstándar(string fecha)
    {
        var fechaDT = DateTime.Parse(fecha, new CultureInfo("cl"));
        var fechaHora = fechaDT.ToString(ISO8006, CultureInfo.InvariantCulture);
        return fechaHora;
    }
}