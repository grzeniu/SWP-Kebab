using System;

namespace AutomaticSpeechRecognition
{
    public interface Handler
    {
        void Handle(RecognizedText text);
    }
}
