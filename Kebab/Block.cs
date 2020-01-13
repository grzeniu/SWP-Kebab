using System;

namespace Kebab
{
    internal class Block
    {
        public Prompt Prompt { get; set; }

        //TODO remove
        public void Execute()
        {
            //Prompt.ReadMessage();

            Environment.Exit(0);
        }
    }
}
