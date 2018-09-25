using System;
using UnityEngine;

public class State {
    void SetInt(string key, int value) {
        PlayerPrefs.SetInt(key, value);
    }

    void SetString(string key, string value) {
        PlayerPrefs.SetString(key, value);
    }

    int GetInt(string key) {
        if (PlayerPrefs.HasKey(key)) {
            return PlayerPrefs.GetInt(key);
        }
        throw new InvalidOperationException("no such key as " + key);
    }

    string GetString(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        throw new InvalidOperationException("no such key as " + key);
    }

    void DeleteAll() {
        PlayerPrefs.DeleteAll();
    }
}
