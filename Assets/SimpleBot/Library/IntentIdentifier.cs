using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SimpleBot;
using SimpleBot.Matcher;

namespace SimpleBot
{
    public class IntentIdentifier
    {
        public const string NO_MATCH_EXIST = "NO_MATCH_EXIST";

        private List<IntentMatcher> matchers;

        public IntentIdentifier(Configuration config)
        {
            this.matchers = config.GetIntentConfigs().Select(c => this.generateMatcher(c)).ToList().ConvertAll(instance => (IntentMatcher)instance); // TODO: support other matchers 
        }

        public IntentMatcher generateMatcher(IntentConfig config) {
            return new VerbatimMatcher(config.Name, config.Patterns());
        }

        public string Identify(string input)
        {
            var matches = this.matchers.Where(matcher => matcher.Match(input) == true);
            if (matches.Count() > 0)
            {
                return matches.First().Name();
            }
            return NO_MATCH_EXIST;
        }
    }
}