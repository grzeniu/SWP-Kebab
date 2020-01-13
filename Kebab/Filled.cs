using System.Collections.Generic;

namespace Kebab
{
    internal class Filled
    {
        public Dictionary<string, string> ConditionsDictionary;

        public Filled()
        {
            ConditionsDictionary = new Dictionary<string, string>();
        }
        public string Execute(string recognizedSpeech)
        {
            return !ConditionsDictionary.ContainsKey(recognizedSpeech) ? ConditionsDictionary["default"] : ConditionsDictionary[recognizedSpeech];
        }
    }
}
