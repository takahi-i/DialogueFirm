using System.Collections.Generic;
using System.Linq;
using System;
using SimpleBot.Matcher;

namespace SimpleBot
{
    public class IntentIdentifier
    {
        public const string NO_MATCH_EXIST = "NO_MATCH_EXIST";

        private List<IntentMatcher> matchers;

        public IntentIdentifier(Configuration config)
        {
            this.matchers = config.GetIntentConfigs().Select(c => this.generateMatcher(c, config.GetTypeConfigs())).ToList().ConvertAll(instance => (IntentMatcher)instance);
        }

        public IntentMatcher generateMatcher(IntentConfig intentConfig, TypeConfig typeConfig) {
            if (intentConfig.MatcherType() == "verbatim")
            {
                return new VerbatimMatcher(intentConfig.Name, intentConfig.Patterns());
            } else if (intentConfig.MatcherType() == "template") {
                return new TemplateMatcher(intentConfig.Name, intentConfig.Patterns(), intentConfig.Slots(), typeConfig);
            } else {
                throw new ArgumentException("No matcher type as " + intentConfig.MatcherType());
            }
        }

        public Intent Identify(string input)
        {
            var results = this.matchers.Select(matcher => matcher.Match(input));
            var matchResults = results.Where(result => result.Success == true);
            if (matchResults.Count() > 0)
            {
                return matchResults.First();
            }
            return new Intent(NO_MATCH_EXIST, false, new Dictionary<string, string>());
        }
    }
}