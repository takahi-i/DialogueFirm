using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using SimpleBot.Matcher;

public class TemplateTest {

    [Test]
    public void MatchTest() {
        var template = new Template("put (?<ingredient>tomato|potate) into (?<method>soup|fly)", new List<string>());   
        Assert.AreEqual(true, template.Match("put tomato into soup").Success);
    }
}
 