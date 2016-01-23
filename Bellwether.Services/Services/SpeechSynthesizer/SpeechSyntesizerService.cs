using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.Services.Services.SpeechSynthesizer
{
    public interface ISpeechSyntesizerService
    {
        Task<bool> ValidateSpeakerAndSpeak(string content);
        bool NowSpeak();
        bool GetSpeakerStatus();
    }
    public class SpeechSyntesizerService : ISpeechSyntesizerService
    {
        private readonly Windows.Media.SpeechSynthesis.SpeechSynthesizer _synthesizer;
        private readonly MediaElement _media;

        public SpeechSyntesizerService()
        {
            _synthesizer = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            _media = new MediaElement();
        }

        public async Task<bool> ValidateSpeakerAndSpeak(string content)
        {
            return !NowSpeak() && await Speak(content);
        }

        public bool NowSpeak()
        {
            if (GetSpeakerStatus())
            {
                _media.Stop();
                return true;
            }
            return false;
        }

        public bool GetSpeakerStatus()
        {
            return _media.CurrentState.Equals(MediaElementState.Playing);
        }

        private async Task<bool> Speak(string content)
        {   
            if (!string.IsNullOrEmpty(content))
            {
                try
                {
                    _synthesizer.Voice = await GetVoice();
                    SpeechSynthesisStream synthesisStream = await _synthesizer.SynthesizeTextToStreamAsync(content);
                    _media.DefaultPlaybackRate = 1.15;
                    _media.AutoPlay = true;
                    _media.SetSource(synthesisStream, synthesisStream.ContentType);
                    _media.Play();
                    return true;
                }
                catch (System.IO.FileNotFoundException)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Media player components unavailable");
                    await messageDialog.ShowAsync();
                    return false;
                }
                catch (Exception)
                {
                    _media.AutoPlay = false;
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Unable to synthesize text");
                    await messageDialog.ShowAsync();
                    return false;
                }             
            }
            return false;
        }

        private async Task<VoiceInformation> GetVoice()
        {
            SettingsViewModel settings = await ServiceFactory.ResourceService.GetAppSettings();
            var voice =
                Windows.Media.SpeechSynthesis.SpeechSynthesizer.AllVoices.FirstOrDefault(
                    x => x.Id == settings.ApplicationVoiceId);
            return voice ?? Windows.Media.SpeechSynthesis.SpeechSynthesizer.AllVoices.FirstOrDefault(
                x => x.Language.Contains(settings.ApplicationLanguage));

        }
    }
}
