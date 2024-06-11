using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NNuku;

public static class Constantes
{
    private static string ISO8006 = "yyyy-MM-ddThh:mm:ss";

    private static string archivoNotas = "diario.ñuku";
    private static string nombreCarpeta = "Ñuku";

    public static Action? Exportar;
    public static Action? MostrarGuardar;
    public static Action? MostrarBorrar;
    public static Action? MostrarError;

    public enum Páginas
    {
        nuevaNota,
        diario,
    }

    public static bool GuardarNota(Nota nota)
    {
        var carpetaDiario = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nombreCarpeta);
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

            MostrarGuardar?.Invoke();
            return true;
        }
        catch
        {
            MostrarError?.Invoke();
            return false;
        }
    }

    public static List<Nota> CargarNotas()
    {
        var carpetaDiario = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nombreCarpeta);
        var archivoDiario = Path.Combine(carpetaDiario, archivoNotas);

        if (!Directory.Exists(carpetaDiario) || !File.Exists(archivoDiario))
            return new List<Nota>();

        try
        {
            // Notas guardadas
            var json = File.ReadAllText(archivoDiario);
            var array = JsonConvert.DeserializeObject<Nota[]>(json);

            if(array != null)
                return array.ToList();
            else
                return new List<Nota>();
        }
        catch
        {
            return new List<Nota>();
        }
    }

    public static void BorrarNota(Nota nota)
    {
        var carpetaDiario = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nombreCarpeta);
        var archivoDiario = Path.Combine(carpetaDiario, archivoNotas);

        if (!Directory.Exists(carpetaDiario) || !File.Exists(archivoDiario))
        {
            MostrarError?.Invoke();
            return;
        }

        try
        {
            // Notas guardadas
            var json = File.ReadAllText(archivoDiario);
            var arreglo = JsonConvert.DeserializeObject<Nota[]>(json);

            var lista = new List<Nota>();
            if (arreglo != null)
                lista = arreglo.ToList();

            // Borra nota
            var fechaEstandar = FormatearFechaEstándar(nota.Fecha);
            var notaEncontrada = lista.Where(o => o.Fecha == fechaEstandar && o.Texto == nota.Texto).FirstOrDefault();

            if (notaEncontrada == null || !lista.Remove(notaEncontrada))
            {
                MostrarError?.Invoke();
                return;
            }

            // Sobreescribe diario
            var jsonNuevo = JsonConvert.SerializeObject(lista);
            File.WriteAllText(archivoDiario, jsonNuevo);
            MostrarBorrar?.Invoke();
        }
        catch
        {
            MostrarError?.Invoke();
            return;
        }
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