using System.IO;
using System.Windows;
using System.Windows.Media;

namespace SocialManager.App.Infrastructure;

public static class ThemeManager
{
    private static readonly string SettingsDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "SocialManager");

    private static readonly string ThemeFile = Path.Combine(SettingsDirectory, "theme.txt");

    public static string CurrentTheme { get; private set; } = "Kid D";

    public static void Initialize()
    {
        var savedTheme = File.Exists(ThemeFile) ? File.ReadAllText(ThemeFile).Trim() : "Kid D";
        Apply(savedTheme, persist: false);
    }

    public static void Apply(string themeName, bool persist = true)
    {
        var palette = themeName switch
        {
            "Instagram" => new Palette(
                "#0B0810", "#110C18", "#17111F", "#21182B", "#352744",
                "#FFF8FC", "#C8B8C8", "#E1306C", "#32101E", "#0F0A14",
                "#181020", "#291B34", "#382040", "#FFFFFF", "#833AB4", "#F77737"),

            "TikTok" => new Palette(
                "#07090A", "#0C1012", "#111618", "#182023", "#283236",
                "#F7FEFF", "#AEC1C6", "#25F4EE", "#092827", "#090D0F",
                "#0E1517", "#172226", "#123034", "#061011", "#FE2C55", "#25F4EE"),

            "YouTube" => new Palette(
                "#090909", "#0F0F0F", "#171717", "#202020", "#333333",
                "#FFFFFF", "#BDBDBD", "#FF0000", "#310000", "#0D0D0D",
                "#121212", "#1C1C1C", "#321010", "#FFFFFF", "#FF0000", "#FFFFFF"),

            _ => new Palette(
                "#080808", "#0D0D0D", "#151515", "#1D1D1D", "#343434",
                "#F7F7F7", "#B8B8B8", "#D71920", "#30090B", "#0A0A0A",
                "#101010", "#1B1B1B", "#321012", "#FFFFFF", "#D71920", "#FFFFFF")
        };

        CurrentTheme = themeName is "Instagram" or "TikTok" or "YouTube" ? themeName : "Kid D";

        SetBrush("AppBackgroundBrush", palette.Background);
        SetBrush("SidebarBrush", palette.Sidebar);
        SetBrush("PanelBrush", palette.Panel);
        SetBrush("PanelAltBrush", palette.PanelAlt);
        SetBrush("BorderBrush", palette.Border);
        SetBrush("TextBrush", palette.Text);
        SetBrush("MutedTextBrush", palette.MutedText);
        SetBrush("AccentBrush", palette.Accent);
        SetBrush("AccentDarkBrush", palette.AccentDark);
        SetBrush("TopBarBrush", palette.TopBar);
        SetBrush("InnerPanelBrush", palette.InnerPanel);
        SetBrush("NavHoverBrush", palette.NavHover);
        SetBrush("NavActiveBrush", palette.NavActive);
        SetBrush("PrimaryForegroundBrush", palette.PrimaryForeground);
        SetBrush("AccentSecondaryBrush", palette.AccentSecondary);
        SetBrush("AccentTertiaryBrush", palette.AccentTertiary);

        Application.Current.Resources["BrandBrush"] = BuildBrandBrush(CurrentTheme, palette);

        if (!persist)
            return;

        Directory.CreateDirectory(SettingsDirectory);
        File.WriteAllText(ThemeFile, CurrentTheme);
    }

    private static void SetBrush(string key, string value) =>
        Application.Current.Resources[key] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(value));

    private static Brush BuildBrandBrush(string themeName, Palette palette)
    {
        if (themeName == "Instagram")
        {
            var brush = new LinearGradientBrush { StartPoint = new Point(0, 1), EndPoint = new Point(1, 0) };
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#833AB4"), 0));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#E1306C"), 0.52));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#F77737"), 1));
            return brush;
        }

        if (themeName == "TikTok")
        {
            var brush = new LinearGradientBrush { StartPoint = new Point(0, 0), EndPoint = new Point(1, 1) };
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#25F4EE"), 0));
            brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FE2C55"), 1));
            return brush;
        }

        return new SolidColorBrush((Color)ColorConverter.ConvertFromString(palette.Accent));
    }

    private sealed record Palette(
        string Background,
        string Sidebar,
        string Panel,
        string PanelAlt,
        string Border,
        string Text,
        string MutedText,
        string Accent,
        string AccentDark,
        string TopBar,
        string InnerPanel,
        string NavHover,
        string NavActive,
        string PrimaryForeground,
        string AccentSecondary,
        string AccentTertiary);
}
