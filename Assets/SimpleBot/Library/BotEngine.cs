using System;
using System.Collections.Generic;
using SimpleBot.Responder;
using UnityEngine;

namespace SimpleBot
{
    public class BotEngine
    {
        private IntentIdentifier identifier;
        private IDictionary<string, ReplyResponder> responders;
        private State state;

        public BotEngine(Configuration config)
        {
            this.identifier = new IntentIdentifier(config);
            this.responders = this.generateResponderMap(config);
            this.state = new State();
        }

        private IDictionary<string, ReplyResponder> generateResponderMap(Configuration config)
        {
            List<ResponderConfig> responderConfigs = config.ResponderConfigs;
            IDictionary<string, ReplyResponder> responderMap = new Dictionary<string, ReplyResponder>();
            foreach (var responderConfig in responderConfigs)
            {
                if (responderConfig.Conditions.Count > 0) {
                    responderMap.Add(responderConfig.Target,
                                     new SimpleResponder(responderConfig.Target, responderConfig.Responds, responderConfig.Conditions[0])); //TODO: support various responders
                } else {
                    responderMap.Add(responderConfig.Target,
                                     new SimpleResponder(responderConfig.Target, responderConfig.Responds, null)); //TODO: support various responders
                }
            }
            return responderMap;
        }

        public Intent IdenfityIntent(string input)
        {
            return this.identifier.Identify(input);
        }

        public string replySentence(string input) {
            Intent intent = this.identifier.Identify(input);
            if (this.responders.ContainsKey(intent.Name))
            {
                return this.responders[intent.name].Respond(intent);
            }

            if (this.responders.ContainsKey("default"))
            {
                return this.responders["default"].Respond(intent);
            }
            throw new InvalidOperationException("No default responder is specified...");
        }
    }
}