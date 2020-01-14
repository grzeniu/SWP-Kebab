using Microsoft.Speech.Recognition;

namespace Kebab
{
    public interface IKebabManager
    {
        void ManageKebab(object sender, SpeechRecognizedEventArgs e);
    }
}