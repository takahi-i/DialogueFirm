using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SimpleBot;
using UnityEngine;
using SimpleBot;

public class IntentIdentifierTest {

    [Test]
    public void IdentifyIntentTest() {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>(){"aho"}, new Dictionary<string, string>()).Build();
        var identifier = new IntentIdentifier(config);
        Assert.AreEqual("foobar", identifier.Identify("aho is a researcher."));
    }

    [Test]
    public void IdentifyNotExist() {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>(){"aho"}, new Dictionary<string, string>()).Build();
        var identifier = new IntentIdentifier(config);
        Assert.AreEqual(IntentIdentifier.NO_MATCH_EXIST, identifier.Identify("perl is not a researcher."));
    }

    [Test]
    public void IdentityMatchWithTemplateMatcher()
    {
        var config = new ConfigurationBuilder().AddIntent("ingredient", "template", new List<string>() {"this is a ${ingredient1}"}, new Dictionary<string, string>(){{"ingredient1", "ingredient"}})
                                               .AddType("ingredient", new List<string>(){"potato", "cherry"} ).Build();
        var identifier = new IntentIdentifier(config);
        Assert.AreEqual("ingredient", identifier.Identify("this is a potato"));
    }

    [Test]
    public void IdentityNotMatchWithTemplateMatcher()
    {
        var config = new ConfigurationBuilder().AddIntent("ingredient", "template", new List<string>() { "this is a ${ingredient1}" }, new Dictionary<string, string>() { { "ingredient1", "ingredient" } })
                                               .AddType("ingredient", new List<string>() { "potato", "cherry" }).Build();
        var identifier = new IntentIdentifier(config);
        Assert.AreEqual(IntentIdentifier.NO_MATCH_EXIST, identifier.Identify("this is a UFO"));
    }
}
