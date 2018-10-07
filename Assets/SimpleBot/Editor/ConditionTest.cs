using System.Collections.Generic;
using NUnit.Framework;
using SimpleBot;
using System;
using UnityEngine;

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

    [Test]
    public void TestUnmatchedTermConditon()
    {
        var config = new ConfigurationBuilder().AddResponds("foobar", new List<string>() { "baz" })
                                               .AddCondition( new ConditionConfig("must", 
                                                                                  new List<ConditionConfig>() { 
                                                                                  new ConditionConfig("term", "feel", new List<Pair>() { new Pair("happy", "dummy") })
                                                                                  })).Build();
        Assert.AreEqual(1, config.ResponderConfigs[0].Conditions.Count);

        Func<State, bool> condition = Condition.Load(config.ResponderConfigs[0].Conditions[0]);
        this.state.SetString("feel", "sad");
        Assert.AreEqual(false, condition(this.state));
    }

    [Test]
    public void TestSimpleRangeCondition()
    {
        var config = new ConfigurationBuilder().AddResponds("foobar", new List<string>() { "baz" }, new List<ConditionConfig>() {
                                                                    new ConditionConfigBuilder().AddType("must").
                                                                          AddChild(new ConditionConfigBuilder().
                                                                                   AddType("range").AddTargetField("angry-level").
                                                                                   AddArgument(new Pair("eq", 1)).
                                                                                   Build())
                                                                    .Build()
                                                                }
                                                           ).Build();
        Assert.AreEqual(1, config.ResponderConfigs[0].Conditions.Count);
        Func<State, bool> condition = Condition.Load(config.ResponderConfigs[0].Conditions[0]);
        this.state.SetInt("angry-level", 1);
        Assert.AreEqual(true, condition(this.state));
    }

    [Test]
    public void TestSimpleUnmatchRangeCondition()
    {
        var config = new ConfigurationBuilder().AddResponds("foobar", new List<string>() { "baz" }, new List<ConditionConfig>() {
                                                                    new ConditionConfigBuilder().AddType("must").
                                                                          AddChild(new ConditionConfigBuilder().
                                                                                   AddType("range").AddTargetField("angry-level").
                                                                                   AddArgument(new Pair("eq", 1)).
                                                                                   Build())
                                                                    .Build()
                                                                }
                                                           ).Build();
        Assert.AreEqual(1, config.ResponderConfigs[0].Conditions.Count);
        Func<State, bool> condition = Condition.Load(config.ResponderConfigs[0].Conditions[0]);
        this.state.SetInt("angry-level", 3);
        Assert.AreEqual(false, condition(this.state));
    }


    [TearDown]
    public void Dispose()
    {
        this.state.DeleteAll();
    }
}
