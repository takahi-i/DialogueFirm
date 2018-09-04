using System;
using System.Collections.Generic;


namespace SimpleBot
{
    namespace Matcher
    {
        public class TemplateMatcher : IntentMatcher
        {
            private string name;

            public TemplateMatcher(string name, List<string> patterns, IDictionary<string, string> slots, TypeConfig typeConfig) {
                this.name = name;
            }

            // pattern: "please give me recipes on #{ingredients} with #{style}"
            // slots: {"ingredient1", "ingredient"}
            // templates: { "ingredinet": ["potato", "tomato"]}
            // see https://github.com/voice-assistant/satori-flow/blob/master/src/matcher/templateIntentMatcher.js#L58
            public Template GenerateTemplate(string pattern, IDictionary<string, string> slots, TypeConfig typeconfig)
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
                            elements.Add(pattern.Substring(startPosition, pattern.Length));
                            break;
                        }
                        elements.Add(pattern.Substring(startPosition, endPosition));
                        inBrace = true;
                    }
                    else // found slot
                    {
                        endPosition = pattern.IndexOf("}", startPosition, StringComparison.Ordinal);
                        var slotName = pattern.Substring(startPosition + 1, endPosition);
                        string typeName = slots[slotName];
                        var typeElements = typeconfig.Get(typeName);
                        elements.Add(string.Join("|", typeElements.ToArray()));
                        slotNames.Add(slotName);
                        inBrace = false;
                    }
                }
                return new Template(string.Join("", elements.ToArray()), slotNames);
            }

            public override bool Match(string input)
            {
                return false;
            }

            public override string Name()
            {
                return this.name;
            }
        }
    }
}
