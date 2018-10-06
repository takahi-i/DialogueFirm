using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using SimpleBot;

public class ConditionConfigTest
{

    [Test]
    public void TestSimpleLoad()
    {
        var config = new ConditionConfigBuilder().
                                                 AddType("must")
                                                 .AddChild(new ConditionConfigBuilder()
                                                          .AddType("term")
                                                          .AddTargetField("status")
                                                          .AddArgument(new Pair("happy", "dummy")).Build())
                                                 .Build();
        Assert.AreEqual("must", config.CondtionType);
    }
}
