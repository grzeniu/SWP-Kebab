using System;
using Microsoft.Speech.Recognition;

namespace AutomaticSpeechRecognition
{
    public class SpeechRecognition : ISpeechRecognition
    {
        private readonly GrammarFactory _grammarFactory = new GrammarFactory();
        private SpeechRecognitionEngine _speechRecognitionEngine;
        private const string GrammarFilePath = @"..\..\..\Grammar\Grammar.xml";

        public bool IsSpeechOn { get; private set; } = true;
        public void StopSpeech() => IsSpeechOn = false;

        public void Initialize(EventHandler<SpeechRecognizedEventArgs> kebabManager)
        {
            //var culture = new CultureInfo("en-US");
            _speechRecognitionEngine = new SpeechRecognitionEngine();
            _speechRecognitionEngine.BabbleTimeout += TimeSpan.FromSeconds(2);
            _speechRecognitionEngine.InitialSilenceTimeout += TimeSpan.FromSeconds(10);
            _speechRecognitionEngine.SetInputToDefaultAudioDevice();
            InitializeGrammars();
            _speechRecognitionEngine.SpeechRecognized += kebabManager;
            _speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void InitializeGrammars()
        {
            Grammar xmlGrammar = _grammarFactory.BuildXmlGrammar(GrammarFilePath);
            //grammar.Enabled = true;
            _speechRecognitionEngine.LoadGrammar(xmlGrammar);
        }
    }
}
