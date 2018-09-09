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
            private Result result;
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

            public Result Match(string input)
            {
                var match = this.pattern.Match(input);
                this.result = new Result(input, match);
                foreach (var name in pattern.GetGroupNames()) {
                    this.slots.Add(name, result.SlotValue(name));
                }
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
