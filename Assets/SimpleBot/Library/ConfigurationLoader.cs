using System;
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
            JsonNode json = JsonNode.Parse(jsonText);
            Debug.Log("hello");
            Debug.Log("hello: " + (string)json["intentions"][0]["name"].Get<string>());
            return new Configuration();
        }
    }
}
