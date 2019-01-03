using System.Collections.Generic;
using System.Linq;
using System;
using DialogFirm.Matcher;
using UnityEngine;

namespace DialogFirm
{
    public class IntentIdentifier
    {
        public const string NO_MATCH_EXIST = "NO_MATCH_EXIST";

        private List<IntentMatcher> matchers;
        private IDictionary<string, List<Effect>> effectMap;

        public IntentIdentifier(Configuration config)
        {
            this.matchers = config.GetIntentConfigs().Select(c => this.generateMatcher(c, config.GetTypeConfigs())).ToList().ConvertAll(instance => (IntentMatcher)instance);
            this.effectMap = this.generateEffectMap(config.GetIntentConfigs());
        }

        private IDictionary<string, List<Effect>> generateEffectMap(List<IntentConfig> intentConfigs)
        {
            Dictionary<string, List<Effect>> effectMap = new Dictionary<string, List<Effect>>();
            foreach (var intentConfig in intentConfigs) {
                string intentName = intentConfig.Name;
                if (!effectMap.ContainsKey(intentName))
                {
                    effectMap[intentName] = new List<Effect>();
                }
                foreach (var effectConfig in intentConfig.Effects) {
                    effectMap[intentName].Add(new Effect(effectConfig));
                }
            }
            return effectMap;
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

        public Intent Identify(string input, State state)
        {
            var loweredInput = input.ToLower();
            var results = this.matchers.Select(matcher => matcher.Match(loweredInput));
            var matchResults = results.Where(result => result.Success == true);
            if (matchResults.Count() > 0)
            {
                var matchedIntent = matchResults.First();
                if (this.effectMap.ContainsKey(matchedIntent.Name)) {
                    foreach (var effect in this.effectMap[matchedIntent.Name]) {
                        effect.Apply(state);
                    }
                }
                if (matchedIntent.Slots.Count > 0) {
                    foreach (var keyvalue in matchedIntent.Slots) {
                        Debug.Log("setting:" + keyvalue.Key);
                        state.SetString(keyvalue.Key, keyvalue.Value);
                    }
                } else {
                    Debug.Log("No slots values...");
                }
                return matchResults.First();
            } else {
                Debug.Log("No matching intent");
            }
            return new Intent(NO_MATCH_EXIST, false, new Dictionary<string, string>());
        }
    }
}