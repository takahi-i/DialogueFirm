using System.Collections.Generic;

namespace SimpleBot
{
    public class IntentConfig
    {
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
        }

        private MatchConfig match;

        public IntentConfig(string name, string type, List<string> patterns)
        {
            this.name = name;
            this.match = new MatchConfig(type, patterns);
        }
    }

    internal class MatchConfig
    {
        public string type;
        public List<string> patterns;

        public MatchConfig(string type, List<string> patterns)
        {
            this.type = type;
            this.patterns = patterns;
        }
    }
}