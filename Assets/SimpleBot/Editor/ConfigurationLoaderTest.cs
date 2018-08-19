﻿using System.Collections;
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
    ]
}
";

        ConfigurationLoader configurationLoader = new ConfigurationLoader();
        configurationLoader.loadFromString(jsonStr);
        //Assert.AreEqual(2, (1 + 3));
    }
}
