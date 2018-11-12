using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.IO;
using SimpleBot;
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
    public Sprite angrySprite;
    public Sprite happySprite;
    private SpriteRenderer spriteRenderer;
    public Image managerImage;

    public void Start()
    {
        Debug.Log("Called Star()");
        angrySprite = Resources.Load<Sprite>("SimpleBot/ReleaseManager/manager-angry") as Sprite;
        happySprite = Resources.Load<Sprite>("SimpleBot/ReleaseManager/manager-happy") as Sprite;
        managerImage.sprite = angrySprite;
    }

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
        string setting = File.ReadAllText(settingFilePath);
        Debug.Log(setting);
        ConfigurationLoader configurationLoader = new ConfigurationLoader();
        Configuration config = configurationLoader.loadFromString(setting);
        this.bot = new BotEngine(config);
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
