using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SimpleBot;
using UnityEngine;
using SimpleBot;

public class IntentIdentifierTest {

    private State state = new State();

    [Test]
    public void IdentifyIntentTest() {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>(){"aho"}, new Dictionary<string, string>()).Build();
        var identifier = new IntentIdentifier(config);
        Assert.AreEqual("foobar", identifier.Identify("aho is a researcher.", state).Name);
    }

    [Test]
    public void IdentifyNotExist() {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>(){"aho"}, new Dictionary<string, string>()).Build();
        var identifier = new IntentIdentifier(config);
        Assert.AreEqual(IntentIdentifier.NO_MATCH_EXIST, identifier.Identify("perl is not a researcher.", state).Name);
    }

    [Test]
    public void IdentityMatchWithTemplateMatcher()
    {
        var config = new ConfigurationBuilder().AddIntent("ingredient", "template", new List<string>() {"this is a ${ingredient1}"}, new Dictionary<string, string>(){{"ingredient1", "ingredient"}})
                                               .AddType("ingredient", new List<string>(){"potato", "cherry"} ).Build();
        var identifier = new IntentIdentifier(config);
        Assert.AreEqual("ingredient", identifier.Identify("this is a potato", state).Name);
    }

    [Test]
    public void SaveSlotValueToStateWithMatchWithTemplateMatcher()
    {
        var config = new ConfigurationBuilder().AddIntent("ingredient", "template", new List<string>() { "this is a ${ingredient1}" }, new Dictionary<string, string>() { { "ingredient1", "ingredient" } })
                                               .AddType("ingredient", new List<string>() { "potato", "cherry" }).Build();
        var identifier = new IntentIdentifier(config);
        Assert.AreEqual("ingredient", identifier.Identify("this is a potato", state).Name);
        Assert.AreEqual("potato", state.GetString("ingredient1"));
    }

    [Test]
    public void IdentityNotMatchWithTemplateMatcher()
    {
        var config = new ConfigurationBuilder().AddIntent("ingredient", "template", new List<string>() { "this is a ${ingredient1}" }, new Dictionary<string, string>() { { "ingredient1", "ingredient" } })
                                               .AddType("ingredient", new List<string>() { "potato", "cherry" }).Build();
        var identifier = new IntentIdentifier(config);
        Assert.AreEqual(IntentIdentifier.NO_MATCH_EXIST, identifier.Identify("this is a UFO", state).Name);
    }
}
