using System.Collections.Generic;
using NUnit.Framework;
using SimpleBot;

public class BotEngineTest {

    [Test]
    public void IdentifyIntent() {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>(){"aho"}, new Dictionary<string ,string>()).Build();
        var engine = new BotEngine(config);
        Assert.AreEqual("foobar", engine.IdenfityIntent("aho is not a researcher.").Name);
    }

    [Test]
    public void ReplySentence()
    {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>() { "aho" }, new Dictionary<string, string>()).
                                               AddResponds("foobar", new List<string>(){"baz"} ).Build();
        var engine = new BotEngine(config);
        Assert.AreEqual("baz", engine.replySentence("aho is not a researcher."));
    }
}
