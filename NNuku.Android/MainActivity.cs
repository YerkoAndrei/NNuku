using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using NNuku.ViewModels;
using static NNuku.Constantes;

namespace NNuku.Android;

[Activity(
    Label = "NNuku.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }

    public override void OnBackPressed()
    {
        switch (MainViewModel.Instancia.Actual)
        {
            case Páginas.nuevaNota:
                MainViewModel.Instancia.GuardarCommand.Execute(null);
                base.OnBackPressed();
                break;
            case Páginas.diario:
                MainViewModel.Instancia.EnClicNotaCommand.Execute(null);
                break;
        }
    }
}
