using System;
using System.Collections.Generic;
using UnityEngine;

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
                        Debug.Log(startPosition);
                        Debug.Log(endPosition);

                        var slotName = pattern.Substring(startPosition + 1, (endPosition - startPosition) -1);
                        Debug.Log("slotname: " + slotName);
                        string typeName = slots[slotName];
                        var typeElements = typeconfig.Get(typeName);
                        Debug.Log(string.Join("|", typeElements.ToArray()));
                        elements.Add(string.Join("|", typeElements.ToArray()));
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
