﻿using System;
using UnityEngine;


namespace DialogFirm
{
    public class State
    {

        public bool HasKey(string key) {
            return PlayerPrefs.HasKey(key);
        }

        public void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public int GetInt(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }
            throw new InvalidOperationException("no such key as " + key);
        }

        public string GetString(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetString(key);
            }
            throw new InvalidOperationException("no such key as " + key);
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}