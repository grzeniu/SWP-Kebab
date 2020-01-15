using Microsoft.Speech.Recognition;

namespace AutomaticSpeechRecognition
{
    //TODO Change class name
    public class RecognizedText
    {
        public RecognizedText(RecognitionEventArgs recognitionResult)
        {
            //TODO map to enum ?
            Meal = recognitionResult.Result.Semantics["dish"].Value.ToString();
            Kind = recognitionResult.Result.Semantics["kind"].Value.ToString();
            Sauce = recognitionResult.Result.Semantics["sauce"].Value.ToString();
            Confidence = recognitionResult.Result.Confidence;
        }

        public string Meal { get; }

        public string Kind { get; }

        public string Sauce { get; }

        public float Confidence { get; }

        public string ToString()
        {
            return Meal + " " + Kind + " " + Sauce + " ";
        }
    }
}
