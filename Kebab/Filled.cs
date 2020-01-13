using System;
using System.Collections.Generic;

namespace Kebab
{
    class Filled
    {
        public Dictionary<String, String> ConditionsDictionary;

        public Filled()
        {
            ConditionsDictionary = new Dictionary<String, String>();
        }
        public String Execute(String recognizedSpeech)
        {
            if (!ConditionsDictionary.ContainsKey(recognizedSpeech))
                return ConditionsDictionary["default"];
            else
                return ConditionsDictionary[recognizedSpeech];
        }
    }
}
