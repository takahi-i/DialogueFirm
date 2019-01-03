using System;
using System.Collections.Generic;

namespace DialogFirm
{
    public class Configuration
    {
        private List<IntentConfig> intentConfigs;
        private TypeConfig typeConfigs;
        private List<ResponderConfig> responderConfigs;

        public List<ResponderConfig> ResponderConfigs
        {
            get
            {
                return responderConfigs;
            }
        }

        public Configuration(List<IntentConfig> intentConfigs, TypeConfig typeConfigs, List<ResponderConfig> responderConfigs)
        {
            this.intentConfigs = intentConfigs;
            this.typeConfigs = typeConfigs;
            this.responderConfigs = responderConfigs;
        }

        public Int32 NumberofIntentions()
        {
            return this.intentConfigs.Count;
        }

        public List<IntentConfig> GetIntentConfigs()
        {
            return this.intentConfigs;
        }

        public List<string> GetTypeConfig(string name)
        {
            return this.typeConfigs.Get(name);
        }

        public TypeConfig GetTypeConfigs() {
            return this.typeConfigs;
        }
    }
}