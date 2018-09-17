using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //System.IO.FileInfo, System.IO.StreamReader, System.IO.StreamWriter
using System; //Exception
using System.Text; //Encoding

public class PornalefController : MonoBehaviour {

    float rotespeed = 0;
	// Use this for initialization
	void Start () {
        this.loadConfig();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {
            rotespeed = 10;
        }
        transform.Rotate(0, 0, this.rotespeed);
	}


    void loadConfig()
    {
        string settingFilePath = Application.dataPath + "/SimpleBot/Scenes/simple-bot-conf.json";
        string setting = File.ReadAllText(settingFilePath);
        Debug.Log(setting);
    }

}
