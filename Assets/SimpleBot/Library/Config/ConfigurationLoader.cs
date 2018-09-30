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
                }
                catch (System.NullReferenceException)
                {
                    // do nothing
                }
                builder.AddIntent(name, type, expressions, slots);
            }

            // extract types
            foreach (var type in json["types"])
            {
                var name = type["name"].Get<string>();
                var values = new List<string>();
                foreach (var value in type["values"])
                {
                    values.Add(value.Get<string>());
                }
                builder.AddType(name, values);
            }

            // extract responders
            foreach (var responderName in json["responders"])
            {
                var targetName = responderName.Get<string>();
                var targetResponders = json["responders"][targetName];
                foreach (var responder in targetResponders)
                {
                    var values = new List<string>();
                    foreach (var respond in responder["responds"])
                    {
                        values.Add(respond.Get<string>());
                    }

                    if (responder.Contains<string>("condition"))
                    {
                        ConditionConfig condition = this.extractCondtion(responder["condition"]);
                        builder.AddResponds(targetName, values, new List<ConditionConfig>() { condition });
                    }
                    else
                    {
                        builder.AddResponds(targetName, values);
                    }
                }
            }
            return builder.Build();
        }

        private ConditionConfig extractCondtion(JsonNode conditionNode)
        {
            foreach (var conditionType in conditionNode)
            {
                string conditonTypeStr = conditionType.Get<string>();
                Debug.Log("conditonTypeStr; " + conditonTypeStr);
                return new  ConditionConfig(conditonTypeStr, this.extractChildCondtions(conditionNode[conditionType.Get<string>()]));
            }
            throw new InvalidOperationException("No conditon is specified...");
        }

        private List<ConditionConfig> extractChildCondtions(JsonNode childConditionListNodes) {
            List<ConditionConfig> childConditions = new List<ConditionConfig>();
            foreach (var conditionNode in childConditionListNodes)
            {
                foreach (var conditionType in conditionNode) {
                    string conditionTypeStr = conditionType.Get<string>();
                    if (conditionTypeStr == "must" || conditionTypeStr == "should") // intermidiate node
                    {
                        childConditions.Add(new ConditionConfig(conditionTypeStr, this.extractChildCondtions(conditionNode[conditionTypeStr])));
                    }
                    else // terminal node
                    {
                        childConditions.Add(this.extractTerminalConditions(conditionTypeStr, conditionNode[conditionTypeStr]));
                    }
                }
            }
            return childConditions;
        }

        private ConditionConfig extractTerminalConditions(string conditionTypeStr, JsonNode terminalConditonNode)
        {
            string conditionFeild = "";
            List<Pair> arguments = new List<Pair>();
            foreach (var field in terminalConditonNode) // NOTE: only one element exist 
            {
                conditionFeild = field.Get<string>();
                foreach (var argumentKey in terminalConditonNode[conditionFeild]) {
                    string keyString = argumentKey.Get<string>();
                    System.Object argValue = terminalConditonNode[conditionFeild][keyString].Get<System.Object>();
                    arguments.Add(new Pair(keyString, argValue));
                }
            }
            return new ConditionConfig(conditionTypeStr, conditionFeild, arguments);
        }
    }
}
