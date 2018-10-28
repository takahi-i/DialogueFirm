﻿using System.IO;
using SimpleBot;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Bot controller.
/// </summary>
public class BotController : MonoBehaviour {


    string str;
    public InputField inputField;
    public Text text;
    private BotEngine bot;

    public void SaveText()
    {
        if (this.bot == null) {
            Debug.Log("loading config");
            this.loadConfig();
        } else {
            Debug.Log("exist config");
        }

        str = inputField.text;
        Debug.Log("input: " + str);
        var reply = this.bot.ReplySentence(str);
        text.text = reply;
        inputField.text = "";
    }

    private void loadConfig()
    {
        string settingFilePath = Application.dataPath + "/SimpleBot/Settings/Guide/guide-conf.json";
        string setting = File.ReadAllText(settingFilePath);
        Debug.Log(setting);
        ConfigurationLoader configurationLoader = new ConfigurationLoader();
        Configuration config = configurationLoader.loadFromString(setting);
        this.bot = new BotEngine(config);
        Debug.Log("Finished loading bot");
    }
}
