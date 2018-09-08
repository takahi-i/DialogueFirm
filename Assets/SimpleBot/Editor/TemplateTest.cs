﻿using NUnit.Framework;
using System.Collections.Generic;
using SimpleBot.Matcher;
using SimpleBot;

public class TemplateTest {

    [Test]
    public void MatchTest() {
        var template = new Template("put (?<ingredient>tomato|potate) into (?<method>soup|fly)", new List<string>());
        var result = template.Match("put tomato into soup");
        Assert.AreEqual(true, result.Success);
        Assert.AreEqual("tomato", result.Match.Groups["ingredient"].Value);
        Assert.AreEqual("soup", result.Match.Groups["method"].Value);
    }
}
 