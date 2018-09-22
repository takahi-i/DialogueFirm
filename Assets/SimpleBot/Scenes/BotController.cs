using UnityEngine;
using UnityEngine.UI;


public class BotController : MonoBehaviour {


    string str;
    public InputField inputField;
    public Text text;

    public void SaveText()
    {
        str = inputField.text;
        text.text = str;
        inputField.text = "";
    }

    // Use this for initialization
    void Start () {
		
	}
	
}
