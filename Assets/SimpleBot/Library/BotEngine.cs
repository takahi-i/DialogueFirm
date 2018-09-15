using System.Collections.Generic;
using SimpleBot.Responder;

namespace SimpleBot
{
    public class BotEngine
    {
        private IntentIdentifier identifier;
        private IDictionary<string, ReplyResponder> responders;

        public BotEngine(Configuration config)
        {
            this.identifier = new IntentIdentifier(config);
        }

        public Intent IdenfityIntent(string input)
        {
            return this.identifier.Identify(input);
        }

        //TODO
        // - Reply sentence
        // - Return properties from Identifier
    }
}