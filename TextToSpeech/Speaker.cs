using Microsoft.Speech.Synthesis;

namespace TextToSpeech
{
    public class Speaker
    {
        private readonly SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();

        public void Initialize()
        {
            _speechSynthesizer.SetOutputToDefaultAudioDevice();
        }

        public void Speak(string textToSpeach)
        {
            _speechSynthesizer.Speak(textToSpeach);
        }

        public void SpeakAsync(string textToSpeach)
        {
            _speechSynthesizer.SpeakAsync(textToSpeach);
        }
    }
}
