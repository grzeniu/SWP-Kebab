using System;
using AutomaticSpeechRecognition;
using Microsoft.Speech.Recognition;

namespace Kebab.Services
{
    internal class KebabManager : IKebabManager
    {
        private readonly ISpeechRecognition _speechRecognition;
        private readonly ITextAnalyzer _textAnalyzer;
        public KebabManager(ISpeechRecognition speechRecognition, ITextAnalyzer textAnalyzer)
        {
            _speechRecognition = speechRecognition;
            _textAnalyzer = textAnalyzer;
        }
        public void ManageKebab(object sender, SpeechRecognizedEventArgs e)
        {
            if (!_speechRecognition.IsSpeechOn) return;
            var result = new RecognizedText(e);

            Console.WriteLine($@"ROZPOZNANO (wiarygodność: {result.Confidence:0.000}): '{result.ToString()}'");
            _textAnalyzer.AnalyzeText(result);
        }
    }
}
