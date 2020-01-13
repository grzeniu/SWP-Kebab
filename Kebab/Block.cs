using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab
{
    class Block
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
