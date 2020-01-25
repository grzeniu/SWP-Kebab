using Microsoft.Speech.Recognition;

namespace Kebab.Services
{
    public interface IKebabManager
    {
        void ManageKebab(object sender, SpeechRecognizedEventArgs e);
    }
}