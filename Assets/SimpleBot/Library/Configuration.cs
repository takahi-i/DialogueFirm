using System.Collections.Generic;

namespace SimpleBot {
    public class Configuration {
        private List<IntentConfig> intentConfigs;

        public Configuration()
        {
        }

        public Configuration(List<IntentConfig> intentConfigs)
        {
            this.intentConfigs = intentConfigs;
        }
    }
}