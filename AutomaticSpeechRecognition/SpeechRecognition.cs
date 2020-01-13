using System;
using System.Globalization;
using Microsoft.Speech.Recognition;

namespace AutomaticSpeechRecognition
{
    public class SpeechRecognition : ISpeechRecognition
    {
        private readonly GrammarFactory _grammarFactory = new GrammarFactory();
        private SpeechRecognitionEngine _speechRecognitionEngine;
        private readonly ITextAnalyzer _textAnalyzer;
        private const string GrammarFilePath = @"C:\Users\Grzesiek\Desktop\swp\SWP-Kebab\AutomaticSpeechRecognition\Grammar\Grammar.xml";
        public SpeechRecognition(ITextAnalyzer textAnalyzer)
        {
            _textAnalyzer = textAnalyzer;
            Initialize();
        }
        private void Initialize()
        {
            var culture = new CultureInfo("en-US");
            _speechRecognitionEngine = new SpeechRecognitionEngine(culture);
            _speechRecognitionEngine.SetInputToDefaultAudioDevice();
            InitializeGrammars();
            _speechRecognitionEngine.SpeechRecognized += KebabManager;
            _speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void InitializeGrammars()
        {
            _grammarFactory.BuildGrammars(_speechRecognitionEngine);

            //TODO change to relative path
            //Grammar grammar = new Grammar("C:\\Users\\Grzesiek\\Desktop\\swp\\SWP-Kebab\\AutomaticSpeechRecognition\\Grammar\\Grammar.xml", "rootRule");
            Grammar xmlGrammar = _grammarFactory.BuildXmlGrammar(GrammarFilePath);
            //grammar.Enabled = true;
            _speechRecognitionEngine.LoadGrammar(xmlGrammar);
        }
        public void KebabManager(object sender, SpeechRecognizedEventArgs e)
        {
            if (!IsSpeechOn) return;
            var result = new RecognizedText(e);

            Console.WriteLine($@"ROZPOZNANO (wiarygodność: {result.Confidence:0.000}): '{result.Text}'");
            _textAnalyzer.AnalyzeText(result);
        }

        public void StopSpeech() => IsSpeechOn = false;

        public bool IsSpeechOn { get; private set; } = true;
    }
}
