using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using NNuku.ViewModels;

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
        MainViewModel.Instancia.EnClicAtrasCommand.Execute(null);
        base.OnBackPressed();
    }
}
