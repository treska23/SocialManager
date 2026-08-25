using System.Windows;
using SocialManager.App.Infrastructure;

namespace SocialManager.App;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        ThemeManager.Initialize();
        base.OnStartup(e);
    }
}
