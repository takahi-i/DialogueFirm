using System.IO;
using SimpleBot;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Bot controller.
/// </summary>
public class GuideController : MonoBehaviour {


    string str;
    public InputField inputField;
    public Text text;
    private BotEngine bot;
    public Sprite confusedSprite;
    public Sprite happySprite;
    public Sprite defaultSprite;
    public Image guideImage;


    public void Start()
    {
        confusedSprite = Resources.Load<Sprite>("SimpleBot/Guide/bulter-confused") as Sprite;
        happySprite = Resources.Load<Sprite>("SimpleBot/Guide/bulter-smile") as Sprite;
        defaultSprite = Resources.Load<Sprite>("SimpleBot/Guide/butler-default") as Sprite;
        guideImage.sprite = defaultSprite;
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

        int failCount = bot.State.GetInt("fail-count");
        Debug.Log("failCount" + failCount.ToString());
        if (failCount == 0)
        {
            guideImage.sprite = happySprite;
        }
        else if (failCount == 1)
        {
            guideImage.sprite = defaultSprite;
        }
        else
        {
            guideImage.sprite = confusedSprite;
        }

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
