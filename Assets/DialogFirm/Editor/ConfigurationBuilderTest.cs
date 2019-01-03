using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using DialogFirm;
using UnityEngine;

public class ConfigurationBuilderTest {

   [Test]
    public void TestBuild() {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>(){"foobar"}, new Dictionary<string, string>()).Build();
        Assert.AreEqual(1, config.NumberofIntentions());
        Assert.AreEqual("foobar", config.GetIntentConfigs()[0].Name);
        Assert.AreEqual("verbatim", config.GetIntentConfigs()[0].MatcherType());
        Assert.AreEqual("foobar", config.GetIntentConfigs()[0].Patterns()[0]);
    }

    [Test]
    public void TestBuildCondition()
    {
        Configuration config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>() { "aho" }, new Dictionary<string, string>()).
                                               AddResponds("foobar", new List<string>() { "baz" },
                                                           new List<ConditionConfig>() {
                                                               new ConditionConfigBuilder().AddType("must").AddChild(
                                                                    new ConditionConfigBuilder().AddType("scope").AddTargetField("age").AddArgument(new Pair("gte", 20)).Build()
                                                              ).Build()
                                                            }
                                                          ).Build();

        Assert.AreEqual(1, config.ResponderConfigs[0].Conditions.Count);
        Assert.AreEqual("must", config.ResponderConfigs[0].Conditions[0].CondtionType);
        Assert.AreEqual(1, config.ResponderConfigs[0].Conditions[0].ChildConfigs.Count);
        Assert.AreEqual("scope", config.ResponderConfigs[0].Conditions[0].ChildConfigs[0].CondtionType);
        Assert.AreEqual("age", config.ResponderConfigs[0].Conditions[0].ChildConfigs[0].TargetField);
        Assert.AreEqual(1, config.ResponderConfigs[0].Conditions[0].ChildConfigs[0].Arguments.Count);
        Assert.AreEqual("gte", config.ResponderConfigs[0].Conditions[0].ChildConfigs[0].Arguments[0].First);
        Assert.AreEqual(20, config.ResponderConfigs[0].Conditions[0].ChildConfigs[0].Arguments[0].Second);
    }

    [Test]
    public void TestBuildConditionWithStringArgument()
    {
        var config = new ConfigurationBuilder().AddIntent("foobar", "verbatim", new List<string>() { "aho" }, new Dictionary<string, string>()).
                                               AddResponds("foobar", new List<string>() { "baz" },
                                                           new List<ConditionConfig>() {
                                                               new ConditionConfigBuilder().AddType("must").AddChild(
                                                                    new ConditionConfigBuilder().AddType("term").AddTargetField("status").AddArgument(new Pair("happy", "dummy")).Build()
                                                              ).Build()
                                                            }
                                                          ).Build();

        Assert.AreEqual(1, config.ResponderConfigs[0].Conditions.Count);
        Assert.AreEqual("must", config.ResponderConfigs[0].Conditions[0].CondtionType);
        Assert.AreEqual(1, config.ResponderConfigs[0].Conditions[0].ChildConfigs.Count);
        Assert.AreEqual("term", config.ResponderConfigs[0].Conditions[0].ChildConfigs[0].CondtionType);
        Assert.AreEqual("status", config.ResponderConfigs[0].Conditions[0].ChildConfigs[0].TargetField);
        Assert.AreEqual(1, config.ResponderConfigs[0].Conditions[0].ChildConfigs[0].Arguments.Count);
        Assert.AreEqual("happy", config.ResponderConfigs[0].Conditions[0].ChildConfigs[0].Arguments[0].First);
        Assert.AreEqual("dummy", config.ResponderConfigs[0].Conditions[0].ChildConfigs[0].Arguments[0].Second);
    }
}
