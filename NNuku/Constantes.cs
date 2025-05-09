﻿using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Globalization;
using System.Collections.Generic;

namespace NNuku;

public static class Constantes
{
    private static string ISO8006 = "yyyy-MM-ddThh:mm:ss";

    private static string archivoNotas = "diario.ñuku";
    private static string nombreCarpeta = "Ñuku";
    public static CultureInfo cultura = new CultureInfo("cl");

    public static Action? Exportar;
    public static Action? MostrarGuardar;
    public static Action? MostrarBorrar;
    public static Action? MostrarEditar;
    public static Action? MostrarError;

    public static Nota NotaEditando = new Nota(string.Empty, string.Empty);

    public enum Páginas
    {
        nuevaNota,
        diario,
        editar
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
            var json = JsonSerializer.Serialize(notas);
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

    public static bool ActualizarNota(Nota nota)
    {
        var carpetaDiario = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nombreCarpeta);
        var archivoDiario = Path.Combine(carpetaDiario, archivoNotas);

        if (!Directory.Exists(carpetaDiario))
            Directory.CreateDirectory(carpetaDiario);

        var notas = CargarNotas();
        var nuevaNota = notas.Where(o => o.Fecha == nota.Fecha).FirstOrDefault();

        if (string.IsNullOrEmpty(nota.Texto) || nuevaNota == null)
        {
            MostrarError?.Invoke();
            return false;
        }

        try
        {
            // Reemplaza texto
            nuevaNota.Texto = nota.Texto;

            // Sobreescribe diario
            var json = JsonSerializer.Serialize(notas);
            File.WriteAllText(archivoDiario, json);

            NotaEditando = new Nota(string.Empty, string.Empty);
            MostrarEditar?.Invoke();
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
            var array = JsonSerializer.Deserialize<Nota[]>(json);

            if (array != null)
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
            var arreglo = JsonSerializer.Deserialize<Nota[]>(json);

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
            var jsonNuevo = JsonSerializer.Serialize(lista);
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
        var día = fecha.ToString("dddd", cultura);

        var charArray = día.ToCharArray();
        charArray[0] = char.ToUpper(charArray[0]);
        día = new string(charArray);

        var fechaHora = fecha.ToString("dd/MM/yyyy  hh:mm:ss", cultura);
        var tiempo = fecha.ToString("tt", cultura).ToLower();

        return día + "  " + fechaHora + " " + tiempo;
    }

    public static string FormatearFechaCorta(string fecha)
    {
        var fechaDT = DateTime.Parse(fecha, cultura);
        var fechaHora = fechaDT.ToString("dd/MM/yyyy  hh:mm:ss", cultura);
        var tiempo = fechaDT.ToString("tt", cultura).ToLower();

        return fechaHora + " " + tiempo;
    }

    public static string FormatearFechaEstándar(DateTime fecha)
    {
        var fechaHora = fecha.ToString(ISO8006);
        return fechaHora;
    }

    public static string FormatearFechaEstándar(string fecha)
    {
        var fechaDT = DateTime.Parse(fecha, cultura);
        var fechaHora = fechaDT.ToString(ISO8006, cultura);
        return fechaHora;
    }
}