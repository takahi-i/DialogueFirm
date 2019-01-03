using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogFirm
{
    namespace Matcher
    {
        public class TemplateMatcher : IntentMatcher
        {
            private string name;
            private IDictionary<string, string> slots;
            private TypeConfig typeconfig;
            private List<Template> templates;

            public TemplateMatcher(string name, List<string> patterns, IDictionary<string, string> slots, TypeConfig typeConfig) {
                this.name = name;
                this.slots = slots;
                this.typeconfig = typeConfig;
                this.templates = new List<Template>();

                foreach (string patternStr in patterns) {
                    this.templates.Add(GenerateTemplate(patternStr, this.slots, typeConfig));
                }
            }

            // pattern: "please give me recipes on #{ingredients} with #{style}"
            // slots: {"ingredient1", "ingredient"}
            // templates: { "ingredinet": ["potato", "tomato"]}
            // see https://github.com/voice-assistant/satori-flow/blob/master/src/matcher/templateIntentMatcher.js#L58
            public static Template GenerateTemplate(string pattern, IDictionary<string, string> slots, TypeConfig typeconfig)
            {
                int startPosition = 0;
                int endPosition = 0;
                var elements = new List<string>();
                var slotNames = new List<string>();
                var inBrace = false;

                while (endPosition < pattern.Length)
                {
                    if (!inBrace)
                    {
                        endPosition = pattern.IndexOf("${", startPosition, StringComparison.Ordinal);
                        if (endPosition == -1)
                        { // no brace
                            elements.Add(pattern.Substring(startPosition, pattern.Length - startPosition));
                            break;
                        }
                        elements.Add(pattern.Substring(startPosition, endPosition - startPosition));
                        inBrace = true;
                    }
                    else // found slot
                    {
                        endPosition = pattern.IndexOf("}", startPosition, StringComparison.Ordinal);
                        var slotName = pattern.Substring(startPosition + 1, (endPosition - startPosition) - 1);
                        string typeName = slots[slotName];
                        var typeElements = typeconfig.Get(typeName);
                        elements.Add(generateSlotElement(typeElements, slotName));
                        slotNames.Add(slotName);
                        inBrace = false;
                    }
                    if (endPosition < 0)
                    {
                        break;
                    }
                    startPosition = endPosition + 1;
                }
                return new Template(string.Join("", elements.ToArray()), slotNames);
            }

            private static string generateSlotElement(List<string> typeElements, string slotName)
            {
                string element = "(?<" + slotName + ">";
                element += string.Join("|", typeElements.Select<string, string>(typeElement => typeElement.ToLower()).ToArray());
                element += ")";
                return element;
            }

            public override Intent Match(string input)
            {
                foreach(var template in this.templates) {
                    Intent result = template.Match(input, this.name);
                    if (result.Success) {
                        return result;
                    }
                }
                return new Intent(input, false, new Dictionary<string, string>());
            }

            public override string Name()
            {
                return this.name;
            }
        }
    }
}
