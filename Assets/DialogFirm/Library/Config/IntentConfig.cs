using System.Collections.Generic;
using UnityEngine;


namespace DialogFirm
{
    public class IntentConfig
    {
        private string name;
        private List<string> patterns;
        private MatchConfig match;
        private List<EffectConfig> effects;

        public List<EffectConfig> Effects
        {
            get
            {
                return effects;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public IntentConfig(string name, string type, List<string> patterns, IDictionary<string, string> slots)
        {
            this.name = name;
            this.match = new MatchConfig(type, patterns, slots);
            this.effects = new List<EffectConfig>();
        }

        public string MatcherType()
        {
            return this.match.MatchType;
        }

        public List<string> Patterns()
        {
            return this.match.Patterns;
        }

        public IDictionary<string, string> Slots()
        {
            return this.match.Slots;
        }

        public void AddEffect(EffectConfig effectConfig) {
            this.effects.Add(effectConfig);
        }
    }

    internal class MatchConfig
    {
        private string matchType;
        private List<string> patterns;
        private IDictionary<string, string> slots;

        public string MatchType
        {
            get
            {
                return this.matchType;
            }
        }

        public List<string> Patterns
        {
            get
            {
                return this.patterns;
            }
        }

        public IDictionary<string, string> Slots
        {
            get
            {
                return slots;
            }
        }

        public MatchConfig(string type, List<string> patterns, IDictionary<string, string> slots)
        {
            this.matchType = type;
            this.patterns = patterns;
            this.slots = slots;
        }
    }

    public class EffectConfig {
        private string targetFIeld;

        public string TargetFIeld
        {
            get
            {
                return targetFIeld;
            }
        }

        private string effectType;

        public string EffectType
        {
            get
            {
                return effectType;
            }
        }

        private object defaultValue;

        public object DefaultValue
        {
            get
            {
                return defaultValue;
            }
        }

        private object setValue;

        public object SetValue
        {
            get
            {
                return setValue;
            }
        }

        private string referField;

        public string ReferField
        {
            get
            {
                return referField;
            }
        }

        public EffectConfig(string targetFIeld, string effectType, object defaultValue, object setvalue, string referField)
        {
            this.targetFIeld = targetFIeld;
            this.effectType = effectType;
            this.defaultValue = defaultValue;
            this.setValue = setvalue;
            this.referField = referField;
        }
    }
}