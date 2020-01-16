using System;

namespace Kebab
{
    internal class Form
    {
        public Field Field { get; set; }
        public Block Block { get; set; }
        public string Id { get; set; }

        public Form(string id)
        {
            Id = id;
        }

        public bool IdEqualsIgnoreCase(string text)
        {
            return string.Equals(Id, text, StringComparison.OrdinalIgnoreCase);
        }
    }
}
