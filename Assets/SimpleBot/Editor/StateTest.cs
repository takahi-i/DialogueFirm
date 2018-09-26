using NUnit.Framework;
using SimpleBot;

public class StateTest {

    private State state = new State();

    [Test]
    public void TestSetInt() {
        this.state.SetInt("foobar", 1);
        Assert.AreEqual(1, this.state.GetInt("foobar"));
    }

    [Test]
    public void TestSetString()
    {
        this.state.SetString("aho", "uho");
        Assert.AreEqual("uho", this.state.GetString("aho"));
    }

    [TearDown]
    public void Dispose()
    {
        this.state.DeleteAll();  
    }
}
