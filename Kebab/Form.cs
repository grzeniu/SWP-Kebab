using System;

namespace Kebab
{
    class Form
    {
        public Field Field { get; set; }
        public Block Block { get; set; }
        public String Id { get; set; }

        public Form(String Id)
        {
            this.Id = Id;
        }
    }
}
