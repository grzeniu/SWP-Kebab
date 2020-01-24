namespace Kebab
{
    internal class Field
    {
        public string Name { get; set; }
        public string GrammarXmlFile { get; set; }
        public Prompt NoMatch { get; set; }
        public Prompt Prompt { get; set; }
        public Filled Filled { get; set; }

        public Field(string name)
        {
            Name = name;
        }
    }
}
