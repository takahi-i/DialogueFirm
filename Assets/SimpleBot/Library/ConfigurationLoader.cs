using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleBot
{
    public class ConfigurationLoader
    {
        private Configuration configuration;

        public ConfigurationLoader()
        {
        }

        public Configuration loadFromFile(string configFilePath)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(configFilePath);
            string jsonText = sr.ReadToEnd();
            return this.loadFromString(jsonText);
        }

        public Configuration loadFromString(string jsonText)
        {
            var intentConfigs = new List<IntentConfig>();
            JsonNode json = JsonNode.Parse(jsonText);
            foreach (var intention in json["intentions"])
            {
                var name = intention["name"].Get<string>();
                var type = intention["match"]["type"].Get<string>();
                var expressions = new List<string>();
                foreach (var expression in intention["match"]["expressions"]) {
                    expressions.Add(expression.Get<string>());
                }
                intentConfigs.Add(new IntentConfig(name, type, expressions));
            }
            return new Configuration();
        }
    }
}
