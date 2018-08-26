using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleBot {
    public class ConfigurationBuilder {
		private List<IntentConfig> IntentConfigs;

        public ConfigurationBuilder()
        {
			IntentConfigs = new List<IntentConfig>();
        }

		public ConfigurationBuilder AddIntent(string name, string type, List<string> patterns) {
			this.IntentConfigs.Add(new IntentConfig(name, type, patterns));
			return this;
		}

		public Configuration Build() {
			return new Configuration(this.IntentConfigs);
		}
    }
}