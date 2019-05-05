using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DialogFirm;

/// <summary>
/// Bot controller.
/// </summary>
public class GuideController : MonoBehaviour {
    public InputField inputField;
    public Text text;
    private BotEngine bot;
    public Sprite confusedSprite;
    public Sprite happySprite;
    public Sprite defaultSprite;
    public Image guideImage;

    public void Start()
    {
        confusedSprite = Resources.Load<Sprite>("DialogFirm/Guide/butler-confused") as Sprite;
        happySprite = Resources.Load<Sprite>("DialogFirm/Guide/butler-smile") as Sprite;
        defaultSprite = Resources.Load<Sprite>("DialogFirm/Guide/butler-default") as Sprite;
        guideImage.sprite = defaultSprite;
        this.LoadConfig();
    }

    public void SaveText()
    {
        var reply = this.bot.ReplySentence(inputField.text);
		this.ChangeImage(bot.State.GetInt("fail-count"));
        text.text = reply;
        inputField.text = "";
    }

	void ChangeImage(int failCount)
	{
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
	}

    void LoadConfig()
    {
        string settingFilePath = this.GetStreamingAssetsPath("DialogFirm/Guide/guide-conf.json");
        string settingString;

        if (Application.platform == RuntimePlatform.Android)
        {
            WWW www = new WWW(settingFilePath);
            while (!www.isDone);
            settingString = www.text;
        }
        else
        {
            settingString = File.ReadAllText(settingFilePath);
        }
        this.bot = new BotEngine(settingString);
    }

    string GetStreamingAssetsPath(string suffix)
    {
        string path;
#if UNITY_EDITOR
     path = Application.dataPath + "/StreamingAssets/";
#elif UNITY_ANDROID
     path = "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IOS
     path = Application.dataPath + "/Raw/";
#else
     //Desktop (Mac OS or Windows)
     path = Application.dataPath + "/StreamingAssets/";
#endif
        path = path + suffix;
        Debug.Log("config path is set to  " + path);
        return path;
    }
}
