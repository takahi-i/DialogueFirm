using System.Collections.Generic;
using UnityEngine;


namespace SimpleBot
{
    public class IntentConfig
    {
        private string name;
        private List<string> patterns;
        private MatchConfig match;

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
}