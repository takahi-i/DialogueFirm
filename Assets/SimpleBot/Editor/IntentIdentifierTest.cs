using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SimpleBot;
using UnityEngine;
using SimpleBot;

public class IntentIdentifierTest {

    [Test]
    public void IdentifyIntentTest() {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>(){"aho"}).Build();
        var identifier = new IntentIdentifier(config);
        Assert.AreEqual("foobar", identifier.Identify("aho is a researcher."));
    }

    [Test]
    public void IdentifyNotExist() {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>(){"aho"}).Build();
        var identifier = new IntentIdentifier(config);
        Assert.AreEqual(IntentIdentifier.NO_MATCH_EXIST, identifier.Identify("perl is not a researcher."));
    }
}
