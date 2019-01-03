using NUnit.Framework;
using DialogFirm;
using System;
using System.Collections.Generic;

public class EffectTest {
    private State state = new State();

    [Test]
    public void TestSimpleApply()
    {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>() { "aho" }, new Dictionary<string, string>())
                                               .AddEffect("angry-level", "incr", 0, null, null).Build();
        Effect effect = new Effect(config.GetIntentConfigs()[0].Effects[0]);
        state.SetInt("angry-level", 0);
        effect.Apply(state);
        Assert.AreEqual(1, state.GetInt("angry-level"));
    }

    [Test]
    public void TestSimpleSetIntApply()
    {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>() { "aho" }, new Dictionary<string, string>())
                                               .AddEffect("angry-level", "set", null, 80, null).Build();
        Effect effect = new Effect(config.GetIntentConfigs()[0].Effects[0]);
        state.SetInt("angry-level", 0);
        effect.Apply(state);
        Assert.AreEqual(80, state.GetInt("angry-level"));
    }

    [Test]
    public void TestSimpleSetStringApply()
    {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>() { "aho" }, new Dictionary<string, string>())
                                               .AddEffect("status", "set", null, "happy", null).Build();
        Effect effect = new Effect(config.GetIntentConfigs()[0].Effects[0]);
        state.SetString("status", "sad");
        effect.Apply(state);
        Assert.AreEqual("happy", state.GetString("status"));
    }


    [Test]
    public void TestSimpleCopyIntFieldApply()
    {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>() { "aho" }, new Dictionary<string, string>())
                                               .AddEffect("prev-status", "copy-sfield", null, null, "status").Build();

        Effect effect = new Effect(config.GetIntentConfigs()[0].Effects[0]);
        state.SetString("status", "sad");
        effect.Apply(state);
        Assert.AreEqual("sad", state.GetString("prev-status"));
    }

}
