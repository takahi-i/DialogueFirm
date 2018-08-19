using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ConfigurationLoaderTest {

    [Test]
    public void ConfigurationLoaderTestSimplePasses() {
        // Use the Assert class to test conditions.
        Assert.AreEqual(2, (1 + 1));
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator ConfigurationLoaderTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
