using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;
using SimpleBot;
using SimpleBot.Matcher;

public class NewTestScript {

    [Test]
    public void TestGenerateTemplate() {
        string pattern = "a ${ingredient1}";
        Dictionary<string, string> slots = new Dictionary<string, string>(){ 
            {"ingredient1", "ingredient"}
        };
        TypeConfig typeconfig = new TypeConfig();
        typeconfig.Add("ingredient", new List<string>() { "potato", "tomato" });
        Template template = TemplateMatcher.GenerateTemplate(pattern, slots, typeconfig);
        Assert.AreEqual("a potato|tomato", template.PatternStr);
    }
}
