using System.Collections.Generic;
using NUnit.Framework;
using DialogFirm;

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
        Assert.AreEqual("baz", engine.ReplySentence("aho is not a researcher."));
    }


    [Test]
    public void NotMatchConditionWithDefault()
    {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>() { "aho" }, new Dictionary<string, string>()).
                                               AddResponds("foobar", new List<string>() { "baz" }).
                                               AddCondition(new ConditionConfig("must",
                                                                                  new List<ConditionConfig>() {
                                                                                  new ConditionConfig("term", "feel", new List<Pair>() { new Pair("happy", "dummy") })
                                                                                })).
                                               AddResponds("default", new List<string>() { "foo" }).
                                               Build();
        var engine = new BotEngine(config);
        Assert.AreEqual("foo", engine.ReplySentence("aho is not a researcher."));
    }

    [Test]
    public void NotMatchConditionWithoutDefault()
    {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>() { "aho" }, new Dictionary<string, string>()).
                                               AddResponds("foobar", new List<string>() { "baz" }).
                                               AddCondition(new ConditionConfig("must",
                                                                                  new List<ConditionConfig>() {
                                                                                  new ConditionConfig("term", "feel", new List<Pair>() { new Pair("happy", "dummy") })
                                                                                })).
                                               Build();
        var engine = new BotEngine(config);

        Assert.That(() => engine.ReplySentence("aho is not a researcher."),
                    Throws.TypeOf<System.InvalidOperationException>());

    }

    [Test]
    public void NotMatchConditionByEffect()
    {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>() { "aho" }, new Dictionary<string, string>()).
                                               AddEffect("angry-level", "incr", 0, null, null).
                                               AddResponds("foobar", new List<string>() { "baz" }).
                                               AddCondition(new ConditionConfig("must",
                                                                                  new List<ConditionConfig>() {
                                                                                  new ConditionConfig("range", "angry-level", new List<Pair>() { new Pair("eq", 1) })
                                                                                })).
                                               AddResponds("default", new List<string>() { "foo" }).
                                               Build();
        var engine = new BotEngine(config);
        Assert.AreEqual("baz", engine.ReplySentence("aho is not a researcher."));
    }

    [Test]
    public void MatchConditionWithPrevStateByEffect()
    {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>() { "aho" }, new Dictionary<string, string>())
                                                .AddEffect("angry-level", "incr", 0, null, null)
                                                .AddEffect("prev-angry-level", "copy-ifield", null, null, "angry-level")
                                                   .AddResponds("foobar", new List<string>() { "baz" })
                                                   .AddCondition(new ConditionConfigBuilder().AddType("must")
                                                   .AddChild(new ConditionConfigBuilder()
                                                        .AddType("range")
                                                        .AddTargetField("angry-level")
                                                        .AddArgument(new Pair("gte", 3)).Build())
                                                    .AddChild(new ConditionConfigBuilder()
                                                        .AddType("range")
                                                        .AddTargetField("prev-angry-level")
                                                        .AddArgument(new Pair("gte", 2)).Build())
                                               .Build())
                                               .AddResponds("default", new List<string>() { "foo" })
                                               .Build();
        var engine = new BotEngine(config);
        Assert.AreEqual("foo", engine.ReplySentence("aho is not a researcher."));
        Assert.AreEqual("foo", engine.ReplySentence("aho is not a researcher."));
        Assert.AreEqual("baz", engine.ReplySentence("aho is not a researcher."));
    }


}
