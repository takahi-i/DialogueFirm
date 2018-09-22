using System.IO;
using SimpleBot;
using UnityEngine;
using UnityEngine.UI;


public class BotController : MonoBehaviour {


    string str;
    public InputField inputField;
    public Text text;
    private BotEngine bot;

    public void SaveText()
    {
        str = inputField.text;
        text.text = str;
        inputField.text = "";
    }

    // Use this for initialization
    void Start () {
        this.loadConfig();
    }

    void loadConfig()
    {
        string settingFilePath = Application.dataPath + "/SimpleBot/Scenes/simple-bot-conf.json";
        string setting = File.ReadAllText(settingFilePath);
        Debug.Log(setting);
        ConfigurationLoader configurationLoader = new ConfigurationLoader();
        Configuration config = configurationLoader.loadFromString(setting);
        this.bot = new BotEngine(config);
    }
}
