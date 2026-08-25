namespace SocialManager.App.Models;

public sealed record NetworkStatus(
    string Name,
    string Handle,
    string Status,
    string StatusColor,
    string AccentColor);
