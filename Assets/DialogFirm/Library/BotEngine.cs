using System;
using System.Collections.Generic;
using DialogFirm.Responder;
using UnityEngine;

namespace DialogFirm
{
    /// <summary>
    /// BotEngine is main class in SimpleBot. This instance load a configuration
    ///  file and manage dialogue between a user and bot.
    /// </summary>
    public class BotEngine
    {
        public float VERSION = 0.7f;
        private IntentIdentifier identifier;
        private IDictionary<string, List<ReplyResponder>> responders;
        private State state;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SimpleBot.BotEngine"/> class.
        /// </summary>
        /// <param name="config">Configuration object loaded from the configuration file</param>
        public BotEngine(Configuration config)
        {
            Debug.Log(String.Format("Generating BotEngine version {0:F}", VERSION));
            setResources(config);
        }

        public BotEngine(string configurationString) {
            Debug.Log(String.Format("Generating BotEngine version {0:F}", VERSION));
            ConfigurationLoader configurationLoader = new ConfigurationLoader();
            Configuration config = configurationLoader.loadFromString(configurationString);
            setResources(config);
        }

        private void setResources(Configuration config) {
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

        /// <summary>
        /// Idenfities the intent of input sentence.
        /// </summary>
        /// <returns>Intent object containing the intent name and extracted properties.</returns>
        /// <param name="input">input sentence</param>
        public Intent IdenfityIntent(string input)
        {
            return this.identifier.Identify(input, this.state);
        }

        public string ReplySentence(string input) {
            Intent intent = this.identifier.Identify(input, this.state);
            if (this.responders.ContainsKey(intent.Name))
            {
                foreach (var responder in this.responders[intent.name]) {
                    if (responder.SatisfyState(this.state)) {
                        return responder.Respond(intent);
                    }
                }
            }

            if (this.responders.ContainsKey("default"))
            {
                return this.responders["default"][0].Respond(intent);
            }
            throw new InvalidOperationException("No default responder is specified...");
        }

        public State State
        {
            get
            {
                return state;
            }
        }
    }
}