using System;
using System.Collections.Generic;
using System.Xml;

namespace Kebab
{
    class Parser
    {
        String documentPath = ".\\Dialogue\\Dialogue.xml";
        List<Form> FormList = new List<Form>();

        public List<Form> ParseDocument()
        {
            XmlTextReader reader = new XmlTextReader(documentPath);
            XmlNodeType type;
            Form FormTag = null;
            String condition = null;

            while (reader.Read())
            {
                type = reader.NodeType;
                if (type == XmlNodeType.Element)
                {
                    if (reader.Name == "form")
                    {
                        FormTag = new Form(reader.GetAttribute("id"));
                        FormList.Add(FormTag);
                    }
                    if (reader.Name == "field")
                    {
                        FormTag.Field = new Field(reader.GetAttribute("name"));
                    }
                    if (reader.Name == "block")
                    {
                        FormTag.Block = new Block();
                    }
                    if (reader.Name == "prompt")
                    {
                        if (FormTag.Field != null && FormTag.Field.Prompt == null)
                            FormTag.Field.Prompt = new Prompt(reader.ReadString());
                        if (FormTag.Block != null && FormTag.Block.Prompt == null)
                        {
                            String Message = reader.ReadString();
                            FormTag.Block.Prompt = new Prompt(Message);
                        }

                    }
                    if (reader.Name == "grammar")
                    {
                        FormTag.Field.GrammarXmlFile = reader.GetAttribute("src");
                    }
                    if (reader.Name == "nomatch")
                    {
                        if (FormTag.Field != null && FormTag.Field.NoMatch == null)
                        {
                            reader.ReadToDescendant("prompt");
                            FormTag.Field.NoMatch = new Prompt(reader.ReadString());
                        }
                    }
                    if (reader.Name == "filled")
                    {
                        FormTag.Field.Filled = new Filled();
                    }
                    if (reader.Name == "if")
                    {
                        condition = reader.GetAttribute("cond").Split('\'', '\'')[1];
                    }
                    if (reader.Name == "elseif")
                    {
                        condition = reader.GetAttribute("cond").Split('\'', '\'')[1];
                    }
                    if (reader.Name == "goto")
                    {
                        FormTag.Field.Filled.ConditionsDictionary.Add(condition, reader.GetAttribute("next").Trim(new Char[] { '#' }));
                        condition = "default";
                    }
                }

            }
            return FormList;
        }
    }
}
