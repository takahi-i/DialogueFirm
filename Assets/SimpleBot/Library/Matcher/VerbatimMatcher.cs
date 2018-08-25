using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace SimpleBot
{
    namespace Matcher
    {
        public class VerbatimMatcher : IntentMatcher
        {
            private HashSet<Regex> patterns;
            private string name;

            public VerbatimMatcher(string name, List<string> patterns)
            {
                this.name = name;
                IEnumerable<Regex> regexPatterns = patterns.Select(x => new Regex(x));
                this.patterns = new HashSet<Regex>(regexPatterns.ToList());
            }

            public override bool Match(string input)
            {
                var matched = patterns.Where(pattern => pattern.IsMatch(input) == true);
                if (matched.Count() >= 0) {
                    return true;
                }
                return false;
            }
        }
    }
}
