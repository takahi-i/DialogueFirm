using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleBot {
    public class ConfigurationBuilder {
		private List<IntentConfig> intentConfigs;
		private TypeConfig typeConfigs;

        public ConfigurationBuilder()
        {
			this.intentConfigs = new List<IntentConfig>();
			this.typeConfigs = new TypeConfig();
        }

		public ConfigurationBuilder AddIntent(string name, string type, List<string> patterns) {
			this.intentConfigs.Add(new IntentConfig(name, type, patterns));
			return this;
		}

		public ConfigurationBuilder AddType(string name, List<string> examples) {
			this.typeConfigs.Add(name, examples);
			return this;
		}

		public Configuration Build() {
			return new Configuration(this.intentConfigs, this.typeConfigs);
		}
    }
}