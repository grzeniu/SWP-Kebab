using AutomaticSpeechRecognition;

namespace Kebab
{
    public interface ITextAnalyzer
    {
        void AnalyzeText(RecognizedText text);
        void ConnectToWindow(MainWindow mainWindow);
    }
}
