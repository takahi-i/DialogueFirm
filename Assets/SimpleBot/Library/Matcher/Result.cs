using System.Text.RegularExpressions;

namespace SimpleBot
{
    namespace Matcher
    {
        public class Result
        {
            private Match match;
            public string input;
            public string Input
            {
                get { return this.input; }
                set { this.input = value; }
            }

            public bool success;
            public bool Success
            {
                get { return this.success; }
                set { this.success = value; }
            }

            public string SlotValue(string slotName) {
                return this.match.Groups[slotName].Value;
            }

            public Result(string input, Match match)
            {
                this.Input = input;
                this.success = match.Success;
                this.match = match;
            }
        }
    }
}