using Microsoft.Speech.Recognition;

namespace AutomaticSpeechRecognition
{
    public interface ISpeechRecognition
    {
        void KebabManager(object sender, SpeechRecognizedEventArgs e);
    }
}