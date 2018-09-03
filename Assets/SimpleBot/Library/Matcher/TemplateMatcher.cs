using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleBot.Matcher;

namespace SimpleBot
{
    namespace Matcher
    {
        public class TemplateMatcher : IntentMatcher
        {
            private string name;

            public TemplateMatcher(string name, List<string> patterns, TypeConfig typeConfig) {
                this.name = name;
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
