﻿namespace NNuku;
using static Constantes;

public partial class MainPage : ContentPage
{
    private bool guardado;

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

        var nuevaNota = new Nota
        {
            Fecha = FormatearFechaEstándar(DateTime.Now),
            Texto = nota.Text
        };

        // Guarda nota
        if (GuardarNota(nuevaNota))
        {
            MostrarPositivo();
            nota.Text = string.Empty;

            Application.Current.Quit();
        }
        else
            MostrarNegativo();
    }

    private void EnClicGuardarYSalir(object sender, EventArgs e)
    {
        CrearNota();
    }

    private void EnClicVerNotas(object sender, EventArgs e)
    {
        if (nota.Text.Length > 0)
        {
            var nuevaNota = new Nota
            {
                Fecha = FormatearFechaEstándar(DateTime.Now),
                Texto = nota.Text
            };

            // Guarda nota
            if (GuardarNota(nuevaNota))
            {
                MostrarPositivo();
                nota.Text = string.Empty;
            }
            else
                MostrarNegativo();
        }

        App.Current.MainPage = new NavigationPage(new Diario());
    }
}
