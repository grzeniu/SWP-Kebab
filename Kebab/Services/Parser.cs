using System.Collections.Generic;
using System.Xml;

namespace Kebab.Services
{
    internal class Parser
    {
        private readonly string _documentPath = @"..\..\..\Dialogue\Dialogue.xml";
        private readonly List<Form> _formList = new List<Form>();

        public List<Form> ParseDocument()
        {
            var reader = new XmlTextReader(_documentPath);
            Form formTag = null;
            string condition = null;

            while (reader.Read())
            {
                XmlNodeType type = reader.NodeType;
                if (type == XmlNodeType.Element)
                {
                    if (reader.Name == "form")
                    {
                        formTag = new Form(reader.GetAttribute("id"));
                        _formList.Add(formTag);
                    }

                    if (formTag == null)
                    {
                        break;
                    }

                    if (reader.Name == "field")
                    {
                        formTag.Field = new Field(reader.GetAttribute("name"));
                    }
                    if (reader.Name == "block")
                    {
                        formTag.Block = new Block();
                    }
                    if (reader.Name == "prompt")
                    {
                        if (formTag.Field != null && formTag.Field.Prompt == null)
                            formTag.Field.Prompt = new Prompt(reader.ReadString());
                        if (formTag.Block != null && formTag.Block.Prompt == null)
                        {
                            var message = reader.ReadString();
                            formTag.Block.Prompt = new Prompt(message);
                        }

                    }
                    if (reader.Name == "grammar")
                    {
                        formTag.Field.GrammarXmlFile = reader.GetAttribute("src");
                    }
                    if (reader.Name == "nomatch")
                    {
                        if (formTag.Field != null && formTag.Field.NoMatch == null)
                        {
                            reader.ReadToDescendant("prompt");
                            formTag.Field.NoMatch = new Prompt(reader.ReadString());
                        }
                    }
                    if (reader.Name == "filled")
                    {
                        formTag.Field.Filled = new Filled();
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
                        formTag.Field.Filled.ConditionsDictionary.Add(condition, reader.GetAttribute("next").Trim(new char[] { '#' }));
                        condition = "default";
                    }
                }
            }
            return _formList;
        }
    }
}
