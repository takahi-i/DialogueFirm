using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace SimpleBot
{
    namespace Matcher
    {
        public class Result
        {
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
                return this.slots[slotName];
            }

            public Result(string input, bool isSuccess, IDictionary<string, string> slots)
            {
                this.Input = input;
                this.success = isSuccess;
                this.slots = slots;
            }

            private IDictionary<string, string> slots;

        }
    }
}