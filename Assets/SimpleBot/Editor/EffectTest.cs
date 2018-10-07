using NUnit.Framework;
using SimpleBot;
using System;
using System.Collections.Generic;

public class EffectTest {
    private State state = new State();

    [Test]
    public void TestSimpleLoad()
    {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>() { "aho" }, new Dictionary<string, string>())
                                               .AddEffect("angry-level", "incr", 0, null).Build();
        Effect effect = new Effect(config.GetIntentConfigs()[0].Effects[0]);
        state.SetInt("angry-level", 0);
        effect.Apply(state);
        Assert.AreEqual(1, state.GetInt("angry-level"));
    }
}
