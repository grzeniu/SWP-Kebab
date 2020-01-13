namespace AutomaticSpeechRecognition
{
    public interface IHandler
    {
        void Handle(RecognizedText text);
    }
}
