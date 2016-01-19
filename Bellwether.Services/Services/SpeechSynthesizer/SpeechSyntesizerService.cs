using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

namespace Bellwether.Services.Services.SpeechSynthesizer
{
    public interface ISpeechSyntesizerService
    {
        Task<bool> Read(string content);
    }
    public class SpeechSyntesizerService:ISpeechSyntesizerService
    {
        private readonly Windows.Media.SpeechSynthesis.SpeechSynthesizer _synthesizer;
        private readonly MediaElement _media;

        public SpeechSyntesizerService()
        {
            _synthesizer = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            _media = new MediaElement();
        }

        public async Task<bool> Read(string content)
        {
            SpeechSynthesisStream synthesisStream = await _synthesizer.SynthesizeTextToStreamAsync(content);
            _media.AutoPlay = true;
            _media.SetSource(synthesisStream, synthesisStream.ContentType);
            _media.Play();
            return true;
        }
    }
}
