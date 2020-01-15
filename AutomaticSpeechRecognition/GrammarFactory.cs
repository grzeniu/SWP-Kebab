using System;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Recognition.SrgsGrammar;

namespace AutomaticSpeechRecognition
{
    internal class GrammarFactory
    {
        public Grammar BuildXmlGrammar(string grammarFilePath)
        {
            var grammarDoc = new SrgsDocument(grammarFilePath);
            return new Grammar(grammarDoc);
        }
    }
}
