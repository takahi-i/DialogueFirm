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
                ""type"" : ""verbatim"",
                ""expressions"" : [ ""tell me"", ""show me"" ]
            }
       }
    ],
    ""types"": [
      {
         ""name"": ""ingredients"",
         ""values"": [""tomato"", ""potate""]
      }
    ]
}
";

        ConfigurationLoader configurationLoader = new ConfigurationLoader();
        var configuration = configurationLoader.loadFromString(jsonStr);
        Assert.AreEqual(2, configuration.NumberofIntentions());

        Assert.AreEqual("repeat", configuration.GetIntentConfigs()[0].Name);
        Assert.AreEqual("verbatim", configuration.GetIntentConfigs()[0].MatcherType());
        Assert.AreEqual("repeat", configuration.GetIntentConfigs()[0].Patterns()[0]);
        Assert.AreEqual("pardon me?", configuration.GetIntentConfigs()[0].Patterns()[1]);

        Assert.AreEqual("question", configuration.GetIntentConfigs()[1].Name);
        Assert.AreEqual("verbatim", configuration.GetIntentConfigs()[1].MatcherType());
        Assert.AreEqual("tell me", configuration.GetIntentConfigs()[1].Patterns()[0]);
        Assert.AreEqual("show me", configuration.GetIntentConfigs()[1].Patterns()[1]);

        Assert.AreEqual(2, configuration.GetType("ingredients").Count);
        Assert.AreEqual("tomato", configuration.GetType("ingredients")[0]);
        Assert.AreEqual("potate", configuration.GetType("ingredients")[1]);
    }
}