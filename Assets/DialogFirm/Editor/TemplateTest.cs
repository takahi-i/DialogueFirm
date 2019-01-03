using NUnit.Framework;
using System.Collections.Generic;
using DialogFirm.Matcher;
using DialogFirm;

public class TemplateTest {

    [Test]
    public void MatchTest() {
        var template = new Template("put (?<ingredient>tomato|potate) into (?<method>soup|fly)", new List<string>());
        var result = template.Match("put tomato into soup", "ingredeient-intent");
        Assert.AreEqual(true, result.Success);
        Assert.AreEqual("tomato", result.SlotValue("ingredient"));
        Assert.AreEqual("soup", result.SlotValue("method"));
    }
}
 