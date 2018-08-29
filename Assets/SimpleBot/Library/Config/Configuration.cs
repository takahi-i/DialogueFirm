using System;
using System.Collections.Generic;
using SimpleBot;

namespace SimpleBot {
    public class Configuration {
        private List<IntentConfig> intentConfigs;
        private TypeConfig typeConfigs;

        public Configuration()
        {
        }

        public Configuration(List<IntentConfig> intentConfigs, TypeConfig typeConfigs)
        {
            this.intentConfigs = intentConfigs;
            this.typeConfigs = typeConfigs;
        }

        public Int32 NumberofIntentions() {
            return this.intentConfigs.Count;
        }

        public List<IntentConfig> GetIntentConfigs() {
            return this.intentConfigs;
        }
    }
}