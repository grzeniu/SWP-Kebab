using System;

namespace Kebab
{
    class Field
    {
        public String Name { get; set; }
        public String GrammarXmlFile { get; set; }
        public Prompt NoMatch { get; set; }
        public Prompt Prompt { get; set; }
        public Filled Filled { get; set; }

        public Field(String Name)
        {
            this.Name = Name;
        }

        public void ExecuteField()
        {
            //Prompt.ReadMessage();
            //ASR_Module.ASR.BuildGrammar(GrammarXmlFile);
            //ASR_Module.ASR.StartRecognition();
        }
    }
}
