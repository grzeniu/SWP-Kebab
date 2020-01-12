using System;
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

        public void Speak(String textToSpeach)
        {
            _speechSynthesizer.Speak(textToSpeach);
        }

        public void SpeakAsync(String textToSpeach)
        {
            _speechSynthesizer.SpeakAsync(textToSpeach);
        }
    }
}
