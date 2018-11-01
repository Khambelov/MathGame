using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.IO;

public class Localization : MonoBehaviour
{
    public static Localization GetLoc;

    private Lang lang;
    public Lang CurrentLang
    {
        get
        {
            return lang;
        }
        set
        {
            lang = value;

            foreach (LoadLocText text in texts)
            {
                text.LoadLoc();
            }
        }
    }

    public List<LoadLocText> texts;

    private JSONNode localization;

    private void Awake()
    {
        GetLoc = this;
        localization = null;
        texts = new List<LoadLocText>();
        lang = PlayerPrefs.HasKey("lang") ? (Lang)PlayerPrefs.GetInt("lang") : Lang.en;
    }

    private void Start()
    {
        localization = JSON.Parse(ReadJsonFile());

        if (localization.Count > 0)
            Preload.preload.locaizationIsReady = true;
    }

    public string GetLocById(int id)
    {
        string s = localization[id.ToString()][lang.ToString()];
        return s.ToUpper();
    }

    string ReadJsonFile()
    {
        return Resources.Load<TextAsset>("Data\\localization").text;
    }
}

public enum Lang
{
    ru,
    en,
    tat
}
