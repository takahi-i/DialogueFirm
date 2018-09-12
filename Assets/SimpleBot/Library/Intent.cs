using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace SimpleBot
{
    public class Intent
    {
        public string name;
        public string Name
        {
            get { return this.name; }
        }

        public bool success;
        public bool Success
        {
            get { return this.success; }
            set { this.success = value; }
        }

        public string SlotValue(string slotName)
        {
            return this.slots[slotName];
        }

        public Intent(string name, bool isSuccess, IDictionary<string, string> slots)
        {
            this.name = name;
            this.success = isSuccess;
            this.slots = slots;
        }

        private IDictionary<string, string> slots;
    }
}