using System.Collections.Generic;
using NUnit.Framework;
using SimpleBot;
using System;

public class ConditionTest
{
    private State state = new State();

    [Test]
    public void TestSimpleLoad()
    {
        var config = new ConfigurationBuilder().AddResponds("foobar", new List<string>() { "baz" },
                                                            new List<ConditionConfig>() { new ConditionConfig("must", new List<ConditionConfig>() { new ConditionConfig("term", "status", new List<Pair>() { new Pair("happy", "dummy") }) }) }).Build();
        Assert.AreEqual(1, config.ResponderConfigs[0].Conditions.Count);

        Func<State, bool> condition = Condition.Load(config.ResponderConfigs[0].Conditions[0]);
        this.state.SetString("status", "happy");
        Assert.AreEqual(true, condition(this.state));
    }

    [TearDown]
    public void Dispose()
    {
        this.state.DeleteAll();
    }
}
