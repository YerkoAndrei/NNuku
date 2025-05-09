﻿using System.IO;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Content.PM;
using Android.Widget;
using Avalonia;
using Avalonia.Android;

namespace NNuku.Android;
using NNuku.ViewModels;
using static NNuku.Constantes;

[Activity(
    Label = "Ñuku",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        MostrarGuardar = MostrarMensajeGuardar;
        MostrarBorrar = MostrarMensajeBorrar;
        MostrarEditar = MostrarMensajeEditar;
        MostrarError = MostrarMensajeError;

        Exportar = ExportarDiario;

        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }

    public override void OnBackPressed()
    {
        switch (MainViewModel.Instancia?.Actual)
        {
            case Páginas.nuevaNota:
                MainViewModel.Instancia.GuardarCommand.Execute(null);
                base.OnBackPressed();
                break;
            case Páginas.diario:
                MainViewModel.Instancia.EnClicNotaCommand.Execute(null);
                break;
            case Páginas.editar:
                MainViewModel.Instancia.ActualizarNotaCommand.Execute(null);
                break;
        }
    }

    public void MostrarMensajeGuardar()
    {
        var notificación = Toast.MakeText(this, "Guardada", ToastLength.Short);
        notificación?.Show();
        Vibrar(200);
    }

    public void MostrarMensajeBorrar()
    {
        var notificación = Toast.MakeText(this, "Borrado", ToastLength.Short);
        notificación?.Show();
        Vibrar(200);
    }

    public void MostrarMensajeEditar()
    {
        var notificación = Toast.MakeText(this, "Editado", ToastLength.Short);
        notificación?.Show();
        Vibrar(200);
    }

    public void MostrarMensajeError()
    {
        var notificación = Toast.MakeText(this, "Error", ToastLength.Short);
        notificación?.Show();
        Vibrar(600);
    }

    public void ExportarDiario()
    {
        // Formato simple
        var formateado = string.Empty;
        var notas = CargarNotas();
        notas = notas.OrderBy(o => o.Fecha).ToList();

        foreach (var nota in notas)
        {
            formateado += FormatearFechaCorta(nota.Fecha) + ": " + nota.Texto;
            formateado += System.Environment.NewLine;
        }

        try
        {
            // Exportar a archivo .txt
            var java = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDocuments);

            var documentos = string.Empty;
            if (java != null)
                documentos = java.ToString();

            var archivo = Path.Combine(documentos, "diario_" + System.DateTime.Now.ToFileTime() + ".txt");
            File.WriteAllText(archivo, formateado);

            var notificación = Toast.MakeText(this, "Exportado: " + documentos, ToastLength.Long);
            notificación?.Show();
            Vibrar(200);
        }
        catch
        {
            MostrarMensajeError();
        }
    }

#pragma warning disable CA1422
#pragma warning disable CA1416
    // Advertencias que se resuelven con el if else
    public void Vibrar(int duración)
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
        {
            var vibrador = GetSystemService(VibratorManagerService) as VibratorManager;
            if (vibrador != null && vibrador.DefaultVibrator.HasVibrator)
                vibrador.DefaultVibrator.Vibrate(duración);
        }
        else if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        {
            var vibrador = Vibrator.FromContext(this);
            if (vibrador != null && vibrador.HasVibrator)
                vibrador.Vibrate(duración);
        }
    }
#pragma warning restore CA1422
#pragma warning restore CA1416
}
