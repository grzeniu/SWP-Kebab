using System;

namespace Kebab
{
    class Prompt
    {
        public String Message { get; set; }

        public Prompt() { }

        public Prompt(String message)
        {
            Message = message;
        }
    }
}
