using System.Collections.Generic;

namespace SimpleBot
{
    namespace Matcher
    {
        class VerbatimMatcher : IntentMatcher
        {
            private HashSet<string> patterns;
            private string name;

            public VerbatimMatcher(string name, List<string> patterns)
            {
              this.name = name;
              this.patterns = new HashSet<string>(patterns);
            }

            override public bool Match(string input)
            {
                return true;
            }
        }
    }
}
