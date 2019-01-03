using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using DialogFirm.Matcher;

public class VerbatimMatcherTest {

    [Test]
    public void TestMatch() {
        VerbatimMatcher matcher = new VerbatimMatcher("foobar", new List<string>(){ "foobar" });
        Assert.AreEqual(true, matcher.Match("this is a foobar").Success);
        Assert.AreEqual(false, matcher.Match("this is a ahor").Success);
    }

    [Test]
    public void TestRegexMatch() {
        var matcher2 = new VerbatimMatcher("foobar", new List<string>(){ "fo.bar" });
        Assert.AreEqual(true, matcher2.Match("this is a foobar").Success);
        Assert.AreEqual(false, matcher2.Match("this is a ahor").Success);
    }
}
