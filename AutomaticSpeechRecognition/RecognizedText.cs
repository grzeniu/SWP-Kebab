using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticSpeechRecognition
{
    public class RecognizedText
    {
        //TODO replace var
        private String _text;
        private List<String> _textList;
        private float _confidence;

        public RecognizedText(String text, List<String> textList, float confidence)
        {
            _text = text;
            _textList = textList;
            _confidence = confidence;
        }

        public string Text
        {
            get => _text;
        }

        public List<String> TextList
        {
            get => _textList;
        }

        public float Confidence
        {
            get => _confidence;
        }
    }
}
