using System;
using Microsoft.Speech.Recognition;
using System.Linq;

namespace AutomaticSpeechRecognition
{
    public class SpeechRecognition
    {
        private readonly GrammarFactory _grammarFactory = new GrammarFactory();
        private SpeechRecognitionEngine _speechRecognitionEngine;
        private bool _speechOn = true;
        private Handler _handler;

        public void Initialize(Handler handler)
        {
            _handler = handler;

            //var culture = new CultureInfo("en-US");
            _speechRecognitionEngine = new SpeechRecognitionEngine();
        
            _speechRecognitionEngine.BabbleTimeout += TimeSpan.FromSeconds(2);
            _speechRecognitionEngine.InitialSilenceTimeout += TimeSpan.FromSeconds(10);
            _speechRecognitionEngine.SetInputToDefaultAudioDevice();
            // _speechSynthesizer.SelectVoice("Microsoft Server Speech Text to Speech Voice (pl-PL, Paulina)");

            _speechRecognitionEngine.SpeechRecognized += KebabManager;
            _speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);

            _grammarFactory.AddGrammars(_speechRecognitionEngine);
        }

        private void KebabManager(object sender, SpeechRecognizedEventArgs e)
        {
            if (!_speechOn) return;
            var text = e.Result.Text;
            var textList = text.Split(' ').ToList();
            var confidence = e.Result.Confidence;
            Console.WriteLine($@"ROZPOZNANO (wiarygodność: {e.Result.Confidence:0.000}): '{text}'");

            RecognizedText result = new RecognizedText(text, textList, confidence);
            _handler.Handle(result);
        }

        public void StopSpeech() => _speechOn = false;

        public bool IsSpeechOn => _speechOn;
    }
}
