using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SimpleBot;
using UnityEngine;

public class ConfigurationLoaderTest
{

    [Test]
    public void LoadConfig()
    {

        string jsonStr = @"{
    ""intentions"": [
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
            }
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
              ""range"": { ""anger-level"": { ""gte"": 3}}
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

        Assert.AreEqual(2, configuration.GetTypeConfig("ingredients").Count);
        Assert.AreEqual("tomato", configuration.GetTypeConfig("ingredients")[0]);
        Assert.AreEqual("potate", configuration.GetTypeConfig("ingredients")[1]);

        Assert.AreEqual(2, configuration.ResponderConfigs.Count);
        Assert.AreEqual("question", configuration.ResponderConfigs[0].Target);
        Assert.AreEqual("Turn the corner and go straight for five minitues.", configuration.ResponderConfigs[0].Responds[0]);
        Assert.AreEqual("default", configuration.ResponderConfigs[1].Target);
        Assert.AreEqual("Sorry I do not understand what you mean.", configuration.ResponderConfigs[1].Responds[0]);
    }
}