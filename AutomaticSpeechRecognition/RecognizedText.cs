using System.Collections.Generic;
using System.Linq;
using Microsoft.Speech.Recognition;

namespace AutomaticSpeechRecognition
{
    public class RecognizedText
    {
        public RecognizedText(RecognitionEventArgs recognitionResult)
        {
            Text = recognitionResult.Result.Text;
            TextList = Text.Split(' ').ToList();
            Confidence = recognitionResult.Result.Confidence;
        }

        public string Text { get; }

        public List<string> TextList { get; }

        public float Confidence { get; }
    }
}
