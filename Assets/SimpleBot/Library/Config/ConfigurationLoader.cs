using System;
using System.Collections;
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
            extractIntents(builder, json);
            extractTypes(builder, json);
            extractResponders(builder, json);
            return builder.Build();
        }

        private static void extractIntents(ConfigurationBuilder builder, JsonNode json)
        {
            foreach (var intent in json["intents"])
            {
                var name = intent["name"].Get<string>();
                var type = intent["match"]["type"].Get<string>();
                var expressions = new List<string>();
                IDictionary<string, string> slots = new Dictionary<string, string>();

                foreach (var expression in intent["match"]["expressions"])
                {
                    expressions.Add(expression.Get<string>());
                }

                if (intent["match"].Contains<string>("slots"))
                {
                    foreach (var slot in intent["match"]["slots"])
                    {
                        slots.Add(slot["name"].Get<string>(), slot["type"].Get<string>());
                    }
                }

                builder.AddIntent(name, type, expressions, slots);

                // handle optional effects
                if (intent.Contains<string>("effects"))
                {
                    foreach (var effect in intent["effects"])
                    {
                        var targetField = effect["field"].Get<string>();
                        var effectType = effect["type"].Get<string>();
                        object defaultValue = null;
                        if (effect.Contains<string>("default"))
                        {
                            defaultValue = effect["default"].Get<object>();
                        }
                        object setValue = null;
                        if (effect.Contains<string>("value"))
                        {
                            setValue = effect["value"].Get<object>();
                        }
                        string referField = null;
                        {
                            referField = effect["refer"].Get<string>();
                        }
                        builder.AddEffect(targetField, effectType, defaultValue, setValue, referField);
                    }
                }
            }
        }

        private void extractResponders(ConfigurationBuilder builder, JsonNode json)
        {
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
                        builder.AddResponds(targetName, values, new List<ConditionConfig>());
                    }
                }
            }
        }

        private static void extractTypes(ConfigurationBuilder builder, JsonNode json)
        {
            if (!json.Contains<string>("types"))
            {
                return;
            }
            // extract types
            foreach (var type in json["types"])
            {
                Debug.Log("come to types block");
                var name = type["name"].Get<string>();
                var values = new List<string>();
                foreach (var value in type["values"])
                {
                    values.Add(value.Get<string>());
                }
                builder.AddType(name, values);
            }
        }

        private ConditionConfig extractCondtion(JsonNode conditionNode)
        {
            foreach (var conditionType in conditionNode)
            {
                string conditonTypeStr = conditionType.Get<string>();
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
                var fieldArguments = terminalConditonNode[conditionFeild].Get<System.Object>();
                if (fieldArguments is IDictionary) // "range": {"status": {"lte": 1}
                { 
                    IDictionary argumentMap = (IDictionary) fieldArguments;
                    foreach (var argumentKey in argumentMap.Keys)
                    {
                        string keyString = (string) argumentKey;
                        System.Object argValue = argumentMap[keyString];
                        arguments.Add(new Pair(keyString, argValue));
                    }
                } else if (fieldArguments is string) { // {"term": {"status": "happy"}}
                    arguments.Add(new Pair((string) fieldArguments, null));
                }
            }
            return new ConditionConfig(conditionTypeStr, conditionFeild, arguments);
        }
    }
}
