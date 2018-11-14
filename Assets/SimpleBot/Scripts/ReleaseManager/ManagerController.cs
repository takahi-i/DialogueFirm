using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.IO;
using SimpleBot;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Bot controller.
/// </summary>
public class ManagerController : MonoBehaviour
{
    string str;
    public InputField inputField;
    public Text text;
    private BotEngine bot;

    public void SaveText()
    {
        if (this.bot == null)
        {
            Debug.Log("loading config");
            this.LoadConfig();
        }
        else
        {
            Debug.Log("exist config");
        }

        str = inputField.text;
        Debug.Log("input: " + str);
        var reply = this.bot.ReplySentence(str);
        text.text = reply;
        inputField.text = "";
    }

    void LoadConfig()
    {
        string settingFilePath = this.GetStreamingAssetsPath("SimpleBot/ReleaseManager/manager-conf.json");
        string settingString = File.ReadAllText(settingFilePath);
        this.bot = new BotEngine(settingString);
        Debug.Log("Finished loading bot");
    }

    string GetStreamingAssetsPath(string suffix)
    {
        string path;
#if UNITY_EDITOR
        path = Application.dataPath + "/StreamingAssets/";
#elif UNITY_ANDROID
     path = "jar:file://"+ Application.dataPath + "!/assets/";
#elif UNITY_IOS
     path = Application.dataPath + "/Raw/";
#else
     //Desktop (Mac OS or Windows)
     path = Application.dataPath + "/StreamingAssets/";
#endif
        path = path + suffix;
        return path;
    }
}
