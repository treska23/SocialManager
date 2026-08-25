using System.Collections.ObjectModel;
using System.Windows.Input;
using SocialManager.App.Infrastructure;
using SocialManager.App.Models;

namespace SocialManager.App.ViewModels;

public sealed class MainViewModel : ObservableObject
{
    private bool _isListening;
    private string _activityMessage = "Añade un dibujo, una canción o una animación para empezar.";

    public MainViewModel()
    {
        Networks = new ObservableCollection<NetworkStatus>
        {
            new("TikTok", "Kid D", "Pendiente de conectar", "#7D8597", "#25F4EE"),
            new("Instagram", "Kid D", "Pendiente de conectar", "#7D8597", "#E1306C"),
            new("YouTube", "Kid D", "Pendiente de conectar", "#7D8597", "#FF453A")
        };

        ToggleVoiceCommand = new RelayCommand(ToggleVoice);
        AddContentCommand = new RelayCommand(() =>
            ActivityMessage = "El selector de archivos será el siguiente módulo que conectemos.");
        OpenPlannerCommand = new RelayCommand(() =>
            ActivityMessage = "El calendario inteligente todavía no tiene publicaciones programadas.");
    }

    public string ProjectName => "KID D";
    public string ProductName => "SOCIAL MANAGER";
    public string ManagerStatus => _isListening ? "Escuchando tu explicación" : "Preparado";
    public string VoiceButtonText => _isListening ? "DETENER ESCUCHA" : "HABLAR CON EL MANAGER";
    public string VoiceHint => _isListening
        ? "Cuéntame de qué trata el contenido."
        : "Explica por voz qué significa la obra y el manager preparará la estrategia.";

    public string QueueCount => "0";
    public string DraftCount => "0";
    public string NextPostPlatform => "SIN PROGRAMAR";
    public string NextPostTitle => "Todavía no hay una próxima publicación";
    public string NextPostTime => "El manager elegirá la mejor franja cuando haya contenido.";

    public ObservableCollection<NetworkStatus> Networks { get; }

    public string ActivityMessage
    {
        get => _activityMessage;
        private set => SetProperty(ref _activityMessage, value);
    }

    public ICommand ToggleVoiceCommand { get; }
    public ICommand AddContentCommand { get; }
    public ICommand OpenPlannerCommand { get; }

    private void ToggleVoice()
    {
        _isListening = !_isListening;
        ActivityMessage = _isListening
            ? "Escuchando. La captura real de audio se conectará al módulo de transcripción local."
            : "Explicación detenida.";

        OnPropertyChanged(nameof(ManagerStatus));
        OnPropertyChanged(nameof(VoiceButtonText));
        OnPropertyChanged(nameof(VoiceHint));
    }
}
