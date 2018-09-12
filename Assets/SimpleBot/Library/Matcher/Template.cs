using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using SimpleBot.Matcher;

namespace SimpleBot
{
    namespace Matcher
    {
        public class Template
        {
            private List<string> slotNames;
            private string patternStr;
            private Regex pattern;
            private Intent result;
            private Dictionary<string, string> slots;

            public Template(string patternStr, List<string> slots)
            {
                this.patternStr = patternStr;
                this.pattern = new Regex(patternStr);
                this.slotNames = slots;
                this.result = null;
                this.slots = new Dictionary<string, string>();
            }

            public string PatternStr
            {
                get
                {
                    return patternStr;
                }
            }

            public Intent Match(string input, string intentName)
            {
                var match = this.pattern.Match(input);
                foreach (var name in pattern.GetGroupNames()) {
                    if (this.slots.ContainsKey(name))
                    { // TODO: need to understand whey this trick is needed
                        continue;
                    }
                    this.slots.Add(name, match.Groups[name].Value);
                }
                this.result = new Intent(intentName, match.Success, this.slots);
                return result;
            }

            public List<string> MatchedSlotNames() {
                return new List<string>(this.slots.Keys);
            }

            public string SlotValue(string slotName) {
                if (this.slots.ContainsKey(slotName)) {
                    return this.slots[slotName];
                }
                return null;
            }
        }
    }
}
