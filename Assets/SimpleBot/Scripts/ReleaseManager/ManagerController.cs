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
    public Sprite bitAngrySprite;
    private SpriteRenderer spriteRenderer;
    public Image managerImage;

    public void Start()
    {
        Debug.Log("Called Star()");
        angrySprite = Resources.Load<Sprite>("SimpleBot/ReleaseManager/manager-angry") as Sprite;
        happySprite = Resources.Load<Sprite>("SimpleBot/ReleaseManager/manager-happy") as Sprite;
        bitAngrySprite = Resources.Load<Sprite>("SimpleBot/ReleaseManager/manager-bit-angry") as Sprite;

        managerImage.sprite = happySprite;
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
        var reply = this.bot.ReplySentence(str);
        int angerLevel = bot.State.GetInt("anger-level");
        if (angerLevel == 0)
        {
            managerImage.sprite = happySprite;
        } else if (angerLevel == 1)
        {
            managerImage.sprite = bitAngrySprite;
        } else  {
            managerImage.sprite = angrySprite;
        }
        text.text = reply;
        inputField.text = "";
}

    void LoadConfig()
    {
        string settingFilePath = this.GetStreamingAssetsPath("SimpleBot/ReleaseManager/manager-conf.json");
        string settingString = File.ReadAllText(settingFilePath);
        this.bot = new BotEngine(settingString);
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
