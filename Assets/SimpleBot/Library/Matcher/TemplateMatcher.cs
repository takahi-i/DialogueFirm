using System.Collections.Generic;

namespace SimpleBot
{
    namespace Matcher
    {
        public class TemplateMatcher : IntentMatcher
        {
            private string name;

            public TemplateMatcher(string name, List<string> patterns, IDictionary<string, string> slots, TypeConfig typeConfig) {
                this.name = name;
            }

            // pattern: "please give me recipes on #{ingredients} with #{style}"
            // slots: {"ingredient1", "ingredient"}
            // templates: { "ingredinet": ["potato", "tomato"]}
            // see https://github.com/voice-assistant/satori-flow/blob/master/src/matcher/templateIntentMatcher.js#L58
            public Template generateTemplate(string pattern, IDictionary<string, string> slots, TypeConfig typeconfig) {
                var startPosition = 0;
                var endPosition = 0;
                var elements = new List<string>();
                var slotNames = new List<string>();
                var inBrace = false;




                return null;
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
