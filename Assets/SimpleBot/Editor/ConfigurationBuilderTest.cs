using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SimpleBot;
using UnityEngine;

public class ConfigurationBuilderTest {

   [Test]
    public void TestBuild() {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>(){"foobar"}).Build();
        Assert.AreEqual(1, config.NumberofIntentions());
        Assert.AreEqual("foobar", config.GetIntentConfigs()[0].Name);
        Assert.AreEqual("verbatim", config.GetIntentConfigs()[0].MatcherType());
        Assert.AreEqual("foobar", config.GetIntentConfigs()[0].Patterns()[0]);
    }
}
