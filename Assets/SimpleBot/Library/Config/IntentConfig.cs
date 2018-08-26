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

        public IntentConfig(string name, string type, List<string> patterns)
        {
            this.name = name;
            this.match = new MatchConfig(type, patterns);
        }

        public string MatcherType() {
            return this.match.MatchType;
        }

        public List<string> Patterns() {
            return this.match.Patterns;
        }
    }

    internal class MatchConfig
    {
        private string matchType;
        private List<string> patterns;

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

        public MatchConfig(string type, List<string> patterns)
        {
            this.matchType = type;
            this.patterns = patterns;
        }
    }
}