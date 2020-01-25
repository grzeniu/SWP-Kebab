using AutomaticSpeechRecognition;

namespace Kebab.Services
{
    public interface ITextAnalyzer
    {
        void AnalyzeText(RecognizedText text);
        void ConnectToWindow(MainWindow mainWindow);
    }
}
