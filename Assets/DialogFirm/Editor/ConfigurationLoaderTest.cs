﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using DialogFirm;
using UnityEngine;

public class ConfigurationLoaderTest
{

    [Test]
    public void LoadConfig()
    {

        string jsonStr = @"{
    ""intents"": [
       {
            ""name"": ""repeat"",
            ""match"" : {
                ""type"" : ""verbatim"",
                ""expressions"" : [ ""repeat"", ""pardon me?"" ]
            }
       },
       {
            ""name"": ""question"",
            ""match"" : {
                ""type"" : ""template"",
                ""slots"" : [ {""name"": ""ingredient1"", ""type"": ""ingredients"" }],
                ""expressions"" : [ ""tell me ${ingredient1}"", ""show me ${ingredient1}"" ]
            },
            ""effects"" : [{
                ""field"": ""anger-level"",
                ""type"": ""set"",
                ""value"": 0}
            ]
       }
    ],
    ""types"": [
      {
         ""name"": ""ingredients"",
         ""values"": [""tomato"", ""potate""]
      }
    ],
    ""responders"": {
      ""question"": [
      {
         ""responds"": [""Turn the corner and go straight for five minitues.""],
         ""condition"": {
           ""must"": [
            {
              ""range"": { ""anger-level"": { ""gte"": 3} }
            }
           ]
         }
      }],
      ""repeat"": [
      {
         ""responds"": [""oh mhy god.""],
         ""condition"": {
           ""must"": [
            {
              ""term"": { ""status"": ""happy"" }
            }
           ]
         }
      }],
      ""default"": [
      {
         ""responds"": [""Sorry I do not understand what you mean.""]
      }
      ]
    }
}
";

        ConfigurationLoader configurationLoader = new ConfigurationLoader();
        var configuration = configurationLoader.loadFromString(jsonStr);
        Assert.AreEqual(2, configuration.NumberofIntentions());

        Assert.AreEqual("repeat", configuration.GetIntentConfigs()[0].Name);
        Assert.AreEqual("verbatim", configuration.GetIntentConfigs()[0].MatcherType());
        Assert.AreEqual("repeat", configuration.GetIntentConfigs()[0].Patterns()[0]);
        Assert.AreEqual("pardon me?", configuration.GetIntentConfigs()[0].Patterns()[1]);
        Assert.AreEqual(0, configuration.GetIntentConfigs()[0].Slots().Count);

        Assert.AreEqual("question", configuration.GetIntentConfigs()[1].Name);
        Assert.AreEqual("template", configuration.GetIntentConfigs()[1].MatcherType());
        Assert.AreEqual("tell me ${ingredient1}", configuration.GetIntentConfigs()[1].Patterns()[0]);
        Assert.AreEqual("show me ${ingredient1}", configuration.GetIntentConfigs()[1].Patterns()[1]);
        Assert.AreEqual(1, configuration.GetIntentConfigs()[1].Slots().Count);
        Assert.AreEqual("ingredients", configuration.GetIntentConfigs()[1].Slots()["ingredient1"]);
        Assert.AreEqual(1, configuration.GetIntentConfigs()[1].Effects.Count);
        Assert.AreEqual("set", configuration.GetIntentConfigs()[1].Effects[0].EffectType);
        Assert.AreEqual(null, configuration.GetIntentConfigs()[1].Effects[0].DefaultValue);
        Assert.AreEqual(0, configuration.GetIntentConfigs()[1].Effects[0].SetValue);

        Assert.AreEqual(2, configuration.GetTypeConfig("ingredients").Count);
        Assert.AreEqual("tomato", configuration.GetTypeConfig("ingredients")[0]);
        Assert.AreEqual("potate", configuration.GetTypeConfig("ingredients")[1]);

        Assert.AreEqual(3, configuration.ResponderConfigs.Count);

        Assert.AreEqual("question", configuration.ResponderConfigs[0].Target);
        Assert.AreEqual("Turn the corner and go straight for five minitues.", configuration.ResponderConfigs[0].Responds[0]);
        Assert.AreEqual(1, configuration.ResponderConfigs[0].Conditions.Count);
        Assert.AreEqual("must", configuration.ResponderConfigs[0].Conditions[0].CondtionType);
        Assert.AreEqual(1, configuration.ResponderConfigs[0].Conditions[0].ChildConfigs.Count);
        Assert.AreEqual("range", configuration.ResponderConfigs[0].Conditions[0].ChildConfigs[0].CondtionType);
        Assert.AreEqual("anger-level", configuration.ResponderConfigs[0].Conditions[0].ChildConfigs[0].TargetField);
        Assert.AreEqual(1, configuration.ResponderConfigs[0].Conditions[0].ChildConfigs[0].Arguments.Count);
        Assert.AreEqual("gte", configuration.ResponderConfigs[0].Conditions[0].ChildConfigs[0].Arguments[0].First);
        Assert.AreEqual(3, configuration.ResponderConfigs[0].Conditions[0].ChildConfigs[0].Arguments[0].Second);

        Assert.AreEqual("repeat", configuration.ResponderConfigs[1].Target);
        Assert.AreEqual("oh mhy god.", configuration.ResponderConfigs[1].Responds[0]);
        Assert.AreEqual(1, configuration.ResponderConfigs[1].Conditions.Count);
        Assert.AreEqual("must", configuration.ResponderConfigs[1].Conditions[0].CondtionType);
        Assert.AreEqual(1, configuration.ResponderConfigs[1].Conditions[0].ChildConfigs.Count);
        Assert.AreEqual("term", configuration.ResponderConfigs[1].Conditions[0].ChildConfigs[0].CondtionType);
        Assert.AreEqual("status", configuration.ResponderConfigs[1].Conditions[0].ChildConfigs[0].TargetField);
        Assert.AreEqual(1, configuration.ResponderConfigs[1].Conditions[0].ChildConfigs[0].Arguments.Count);
        Assert.AreEqual("happy", configuration.ResponderConfigs[1].Conditions[0].ChildConfigs[0].Arguments[0].First);
        Assert.AreEqual(null, configuration.ResponderConfigs[1].Conditions[0].ChildConfigs[0].Arguments[0].Second);

        Assert.AreEqual("default", configuration.ResponderConfigs[2].Target);
        Assert.AreEqual("Sorry I do not understand what you mean.", configuration.ResponderConfigs[2].Responds[0]);
    }
}