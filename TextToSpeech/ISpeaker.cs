namespace TextToSpeech
{
    public interface ISpeaker
    {
        void Speak(string textToSpeech);
        void SpeakAsync(string textToSpeech);
    }
}