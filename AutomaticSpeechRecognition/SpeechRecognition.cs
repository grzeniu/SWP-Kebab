using System;
using System.Globalization;
using System.Linq;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Recognition.SrgsGrammar;

namespace AutomaticSpeechRecognition
{
    public class SpeechRecognition
    {
        private readonly GrammarFactory _grammarFactory = new GrammarFactory();
        private SpeechRecognitionEngine _speechRecognitionEngine;
        private IHandler _handler;

        public void Initialize(IHandler handler)
        {
            _handler = handler;

            var culture = new CultureInfo("en-US");
            _speechRecognitionEngine = new SpeechRecognitionEngine(culture);

            _speechRecognitionEngine.SetInputToDefaultAudioDevice();
            // _speechSynthesizer.SelectVoice("Microsoft Server Speech Text to Speech Voice (pl-PL, Paulina)");
            //_grammarFactory.AddGrammars(_speechRecognitionEngine);

            //TODO change to relative path
            //Grammar grammar = new Grammar("C:\\Users\\Grzesiek\\Desktop\\swp\\SWP-Kebab\\AutomaticSpeechRecognition\\Grammar\\Grammar.xml", "rootRule");
            string grammarFilePath = "C:\\Users\\Grzesiek\\Desktop\\swp\\SWP-Kebab\\AutomaticSpeechRecognition\\Grammar\\Grammar.xml";
            SrgsDocument grammarDoc = new SrgsDocument(grammarFilePath);
            Grammar grammar = new Grammar(grammarDoc);
            //grammar.Enabled = true;
            _speechRecognitionEngine.LoadGrammar(grammar);

            _speechRecognitionEngine.SpeechRecognized += KebabManager;
            _speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void KebabManager(object sender, SpeechRecognizedEventArgs e)
        {
            if (!IsSpeechOn) return;
            var text = e.Result.Text;
            var textList = text.Split(' ').ToList();
            var confidence = e.Result.Confidence;
            Console.WriteLine($@"ROZPOZNANO (wiarygodność: {e.Result.Confidence:0.000}): '{text}'");

            RecognizedText result = new RecognizedText(text, textList, confidence);
            _handler.Handle(result);
        }

        public void StopSpeech() => IsSpeechOn = false;

        public bool IsSpeechOn { get; private set; } = true;
    }
}
