using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
//using CommunityToolkit.Maui.Alerts;
//using CommunityToolkit.Maui.Core;
using Newtonsoft.Json;

namespace NNuku;

public static class Constantes
{
    private static string ISO8006 = "yyyy-MM-ddThh:mm:ss";

    private static string archivoNotas = "diario.ñuku";
    private static string nombreCarpeta = "Ñuku";

    public enum Páginas
    {
        nuevaNota,
        diario,
    }

    public static void MostrarPositivo()
    {
        //Vibration.Default.Vibrate(TimeSpan.FromSeconds(0.2));
        //MostrarMensaje("Guardado");
    }

    public static void MostrarNegativo()
    {
        //Vibration.Default.Vibrate(TimeSpan.FromSeconds(1));
        //MostrarMensaje("Error");
    }

    private static async void MostrarMensaje(string mensaje)
    {
        //var cancellationTokenSource = new CancellationTokenSource();
        //var toast = Toast.Make(mensaje, ToastDuration.Short);
        //await toast.Show(cancellationTokenSource.Token);        
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

    public static bool BorrarNota(Nota nota)
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
        // Formato simple
        var formato = string.Empty;
        var notas = CargarNotas();
        notas = notas.OrderByDescending(o => o.Fecha).ToList();

        foreach (var nota in notas)
        {
            formato += FormatearFechaCorta(nota.Fecha) + ": " + nota.Texto;
            formato += Environment.NewLine;
        }

        // PENDIENTE:
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

        var fechaHora = fecha.ToString("dd/MM/yyyy  hh:mm:ss", CultureInfo.CurrentCulture);
        var tiempo = fecha.ToString("tt", CultureInfo.CurrentCulture).ToLower();

        return día + "  " + fechaHora + " " + tiempo;
    }

    public static string FormatearFechaCorta(string fecha)
    {
        var fechaDT = DateTime.Parse(fecha, new CultureInfo("cl"));
        var fechaHora = fechaDT.ToString("dd/MM/yyyy  hh:mm:ss", CultureInfo.CurrentCulture);
        var tiempo = fechaDT.ToString("tt", CultureInfo.CurrentCulture).ToLower();

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
        var fechaHora = fechaDT.ToString(ISO8006, CultureInfo.CurrentCulture);
        return fechaHora;
    }
}