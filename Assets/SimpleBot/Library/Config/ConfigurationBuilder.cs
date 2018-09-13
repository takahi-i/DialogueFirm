using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleBot {
    public class ConfigurationBuilder
    {
        private List<IntentConfig> intentConfigs;
        private TypeConfig typeConfigs;
        private List<ResponderConfig> responderConfigs;

        public ConfigurationBuilder()
        {
            this.intentConfigs = new List<IntentConfig>();
            this.typeConfigs = new TypeConfig();
        }

        public ConfigurationBuilder AddIntent(string name, string type, List<string> patterns, IDictionary<string, string> slots)
        {
            this.intentConfigs.Add(new IntentConfig(name, type, patterns, slots));
            return this;
        }

        public ConfigurationBuilder AddType(string name, List<string> examples)
        {
            this.typeConfigs.Add(name, examples);
            return this;
        }

        public Configuration Build()
        {
            return new Configuration(this.intentConfigs, this.typeConfigs);
        }

        public ConfigurationBuilder AddResponds(string target, List<string> responds)
        {
            this.responderConfigs.Add(new ResponderConfig(target, responds));
            return this;
        }
    }
}