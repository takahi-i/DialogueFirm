using System;
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
            this.responders = this.generateResponderMap(config);
        }

        private IDictionary<string, ReplyResponder> generateResponderMap(Configuration config)
        {
            List<ResponderConfig> responderConfigs = config.ResponderConfigs;
            IDictionary<string, ReplyResponder> responderMap = new Dictionary<string, ReplyResponder>();
            foreach (var responderConfig in responderConfigs)
            {
                responderMap.Add(responderConfig.Target, new SimpleResponder(responderConfig.Target, responderConfig.Responds)); //TODO: support various responders
            }
            return responderMap;
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