using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;
using DialogFirm;
using DialogFirm.Matcher;

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
    public void TestCannotGenerateTemplateWithoutTemplate()
    {
        string pattern = "a ${ingredient1}";
        Dictionary<string, string> slots = new Dictionary<string, string>(){
            {"ingredient1", "ingredient"}
        };
        TypeConfig typeconfig = new TypeConfig();
        Assert.That(() => TemplateMatcher.GenerateTemplate(pattern, slots, typeconfig),
                    Throws.TypeOf<System.InvalidOperationException>());
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
        Intent result = template.Match("this is a potato or not.", "ingredient-intent");
        Assert.AreEqual(true, result.Success);
        Assert.AreEqual("potato", result.SlotValue("ingredient1"));
    }

    [Test]
    public void TestGenerateTemplateWithMultipleMatch()
    {
        string pattern = "${ingredient1} and ${ingredient2} are good";
        Dictionary<string, string> slots = new Dictionary<string, string>(){
            {"ingredient1", "ingredient"},
            {"ingredient2", "ingredient"}
        };
        TypeConfig typeconfig = new TypeConfig();
        typeconfig.Add("ingredient", new List<string>() { "potato", "tomato" });
        Template template = TemplateMatcher.GenerateTemplate(pattern, slots, typeconfig);
        Assert.AreEqual("(?<ingredient1>potato|tomato) and (?<ingredient2>potato|tomato) are good", template.PatternStr);
        Intent result = template.Match("potato and tomato are good enough", "ingredient-intent");
        Assert.AreEqual(true, result.Success);
        Assert.AreEqual("potato", result.SlotValue("ingredient1"));
        Assert.AreEqual("tomato", result.SlotValue("ingredient2"));

    }

    [Test]
    public void TestGenerateTemplateWithJapaneseMatch()
    {
        string pattern = "${ingredient1}は好き";
        Dictionary<string, string> slots = new Dictionary<string, string>(){
            {"ingredient1", "ingredient"}
        };
        TypeConfig typeconfig = new TypeConfig();
        typeconfig.Add("ingredient", new List<string>() { "ポテト", "トマト" });
        Template template = TemplateMatcher.GenerateTemplate(pattern, slots, typeconfig);
        Assert.AreEqual("(?<ingredient1>ポテト|トマト)は好き", template.PatternStr);
        Intent result = template.Match("美味しいポテトは好きですか？", "ingredient-intent");
        Assert.AreEqual(true, result.Success);
        Assert.AreEqual("ポテト", result.SlotValue("ingredient1"));
    }
}
