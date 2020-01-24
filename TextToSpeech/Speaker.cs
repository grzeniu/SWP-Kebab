using Microsoft.Speech.Synthesis;
using System;

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
            Console.WriteLine("mowie "+textToSpeech);
            _speechSynthesizer.Speak(textToSpeech);
        }

        public void SpeakAsync(string textToSpeech)
        {
            _speechSynthesizer.SpeakAsync(textToSpeech);
        }
    }
}
