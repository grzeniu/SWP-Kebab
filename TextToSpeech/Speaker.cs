using Microsoft.Speech.Synthesis;

namespace TextToSpeech
{
    public class Speaker : ISpeaker
    {
        private readonly SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();

        public Speaker()
        {
            Initialize();
        }
        private void Initialize()
        {
            _speechSynthesizer.SetOutputToDefaultAudioDevice();
        }

        public void Speak(string textToSpeech)
        {
            _speechSynthesizer.Speak(textToSpeech);
        }

        public void SpeakAsync(string textToSpeech)
        {
            _speechSynthesizer.SpeakAsync(textToSpeech);
        }
    }
}
