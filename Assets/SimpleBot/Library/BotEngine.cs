using System;
using System.Collections.Generic;
using SimpleBot.Responder;
using UnityEngine;

namespace SimpleBot
{
    public class BotEngine
    {
        private IntentIdentifier identifier;
        private IDictionary<string, List<ReplyResponder>> responders;
        private State state;

        public BotEngine(Configuration config)
        {
            this.identifier = new IntentIdentifier(config);
            this.responders = this.generateResponderMap(config);
            this.state = new State();
        }

        private IDictionary<string, List<ReplyResponder>> generateResponderMap(Configuration config)
        {
            List<ResponderConfig> responderConfigs = config.ResponderConfigs;
            IDictionary<string, List<ReplyResponder>> responderMap = new Dictionary<string, List<ReplyResponder>>();
            foreach (var responderConfig in responderConfigs)
            {
                string target = responderConfig.Target;
                if (!responderMap.ContainsKey(target)) {
                    responderMap[target] = new List<ReplyResponder>();
                }


                if (responderConfig.Conditions.Count > 0) {
                    responderMap[target].Add(new SimpleResponder(responderConfig.Target, responderConfig.Responds, responderConfig.Conditions[0])); //TODO: support various responders
                } else {
                    responderMap[target].Add(new SimpleResponder(responderConfig.Target, responderConfig.Responds, null)); //TODO: support various responders
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
                foreach (var responder in this.responders[intent.name]) {
                    return responder.Respond(intent);
                }
            }

            if (this.responders.ContainsKey("default"))
            {
                return this.responders["default"][0].Respond(intent);
            }
            throw new InvalidOperationException("No default responder is specified...");
        }
    }
}