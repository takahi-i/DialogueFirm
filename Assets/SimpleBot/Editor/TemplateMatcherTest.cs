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
        Assert.AreEqual("a (?<ingredient1>potato|tomato)", template.PatternStr);
    }

    [Test]
    public void TestGenerateTemplateWithMatch()
    {
        string pattern = "a ${ingredient1}";
        Dictionary<string, string> slots = new Dictionary<string, string>(){
            {"ingredient1", "ingredient"}
        };
        TypeConfig typeconfig = new TypeConfig();
        typeconfig.Add("ingredient", new List<string>() { "potato", "tomato" });
        Template template = TemplateMatcher.GenerateTemplate(pattern, slots, typeconfig);
        Assert.AreEqual("a (?<ingredient1>potato|tomato)", template.PatternStr);
        Result result = template.Match("this is a potato or not.");
        Assert.AreEqual(true, result.Success);
        Assert.AreEqual("potato", result.SlotValue("ingredient1"));
    }
}
