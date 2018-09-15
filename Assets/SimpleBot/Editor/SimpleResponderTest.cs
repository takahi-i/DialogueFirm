using NUnit.Framework;
using System.Collections.Generic;
using SimpleBot.Responder;
public class SimpleResponderTest {

    [Test]
    public void RespondToIntent() {
        SimpleResponder responder = new SimpleResponder("ingredient", new List<string>() { "yes that is very tasty."});
        Assert.AreEqual("yes that is very tasty.", responder.Respond(new SimpleBot.Intent("ingredient", true, new Dictionary<string, string>())));
    }
}
