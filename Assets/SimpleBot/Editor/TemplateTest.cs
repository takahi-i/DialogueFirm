using NUnit.Framework;
using System.Collections.Generic;
using SimpleBot.Matcher;
using SimpleBot;

public class TemplateTest {

    [Test]
    public void MatchTest() {
        var template = new Template("put (?<ingredient>tomato|potate) into (?<method>soup|fly)", new List<string>());   
        Assert.AreEqual(true, template.Match("put tomato into soup").Success);
    }
}
 