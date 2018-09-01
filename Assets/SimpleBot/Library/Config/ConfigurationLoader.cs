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
            var builder = new ConfigurationBuilder();
            JsonNode json = JsonNode.Parse(jsonText);

            // extract intents
            foreach (var intention in json["intentions"])
            {
                var name = intention["name"].Get<string>();
                var type = intention["match"]["type"].Get<string>();
                var expressions = new List<string>();
                IDictionary<string, string> slots = new Dictionary<string, string>();

                foreach (var expression in intention["match"]["expressions"])
                {
                    expressions.Add(expression.Get<string>());
                }

                try
                {
                    foreach (var slot in intention["match"]["slots"])
                    {
                        slots.Add(slot["name"].Get<string>(), slot["type"].Get<string>());
                    }
                } catch (System.NullReferenceException) {
                    // do nothing
                }
                builder.AddIntent(name, type, expressions, slots);
            }

            // extract types
            foreach (var type in json["types"]) {
                var name = type["name"].Get<string>();
                var values = new List<string>();
                foreach (var value in type["values"])
                {
                    values.Add(value.Get<string>());
                }
                builder.AddType(name, values);
            }
            return builder.Build();
        }
    }
}
