using System;
using Microsoft.Speech.Recognition;

namespace AutomaticSpeechRecognition
{
    public interface ISpeechRecognition
    {
        void Initialize(EventHandler<SpeechRecognizedEventArgs> kebabManager);
        void StopSpeech();
        bool IsSpeechOn { get; }
    }
}