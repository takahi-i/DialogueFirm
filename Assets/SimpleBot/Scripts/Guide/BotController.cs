using System.IO;
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
    public Sprite angrySprite;
    public Sprite happySprite;
    public Sprite bitAngrySprite;
    public Image guideImage;


    public void Start()
    {
        Debug.Log("Called Star()");
        angrySprite = Resources.Load<Sprite>("SimpleBot/Guide/bulter-confused") as Sprite;
        happySprite = Resources.Load<Sprite>("SimpleBot/Guide/bulter-smile") as Sprite;
        bitAngrySprite = Resources.Load<Sprite>("SimpleBot/Guide/butler-default") as Sprite;
        guideImage.sprite = happySprite;
    }

    public void SaveText()
    {
        if (this.bot == null) {
            Debug.Log("loading config");
            this.LoadConfig();
        } else {
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
        
        string settingFilePath = this.GetStreamingAssetsPath("SimpleBot/Guide/guide-conf.json");
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
        Debug.Log("Path of setting file: " + path);
        return path;
    }
}
