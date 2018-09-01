using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SimpleBot;
using UnityEngine;

public class BotEngineTest {

    [Test]
    public void IdentifyIntent() {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>(){"aho"}, new Dictionary<string ,string>()).Build();
        var engine = new BotEngine(config);
        Assert.AreEqual("foobar", engine.IdenfityIntent("aho is not a researcher."));
    }
}
