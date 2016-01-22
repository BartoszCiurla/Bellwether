using Windows.Media.SpeechSynthesis;

namespace Bellwether.ViewModels
{
    public class VoiceViewModel
    {
        public string DisplayName => Voice.DisplayName;
        public string Language => Voice.Language;
        public string VoiceId => Voice.Id;
        public VoiceInformation Voice { get; set; }
    }
}
