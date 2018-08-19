using NUnit.Framework;
using SimpleBot;

public class ConfigurationLoaderTest
{

    [Test]
    public void LoadConfig()
    {
        ConfigurationLoader configurationLoader = new ConfigurationLoader();
        configurationLoader.loadFromString("{\"name\": \"phi\", \"age\": 26, \"bloodType\": \"O\"}");
        //Assert.AreEqual(2, (1 + 3));
    }
}