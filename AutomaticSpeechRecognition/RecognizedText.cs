using System.Collections.Generic;

namespace AutomaticSpeechRecognition
{
    public class RecognizedText
    {
        //TODO replace var

        public RecognizedText(string text, List<string> textList, float confidence)
        {
            Text = text;
            TextList = textList;
            Confidence = confidence;
        }

        public string Text { get; }

        public List<string> TextList { get; }

        public float Confidence { get; }
    }
}
