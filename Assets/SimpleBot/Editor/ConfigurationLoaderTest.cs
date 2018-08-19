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
            ""pattern"" : {
                ""type"" : ""verbatim"",
                ""expression"" : [ ""repeat"", ""pardon me?"" ]
            }
        }
    ]
}
";
        //Debug.Log(jsonStr.Length);
        //Debug.Log("length");
        ConfigurationLoader configurationLoader = new ConfigurationLoader();
        configurationLoader.loadFromString(jsonStr);
        //Assert.AreEqual(2, (1 + 3));
    }
}
