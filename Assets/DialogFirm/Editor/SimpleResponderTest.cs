using NUnit.Framework;
using System.Collections.Generic;
using DialogFirm.Responder;
public class SimpleResponderTest {

    [Test]
    public void RespondToIntent() {
        SimpleResponder responder = new SimpleResponder(
            "ingredient", new List<string>() { "yes that is very tasty."}, null
        );
        Assert.AreEqual("yes that is very tasty.", responder.Respond(new DialogFirm.Intent("ingredient", true, new Dictionary<string, string>())));
    }

    [Test]
    public void RespondToNotToMatchIntent()
    {
        SimpleResponder responder = new SimpleResponder(
            "ingredient", new List<string>(), null
        );

        Assert.That(() => responder.Respond(new DialogFirm.Intent("not-ingredient", true, new Dictionary<string, string>())),
            Throws.TypeOf<System.InvalidOperationException>());
    }
}
