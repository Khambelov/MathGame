using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{

    public Toggle audio;
    public Toggle window;

    public void Start()
    {
        AudioListener.volume = PlayerPrefs.GetInt("audioStg");
        audio.isOn = PlayerPrefs.GetInt("audioStg") == 1 ? true : false;

        window.isOn = PlayerPrefs.GetInt("windowStg") == 1 ? true : false;
        Screen.fullScreen = window.isOn;
    }

    public void Play(string mode)
    {
        Globals.global.Mode = mode;
        Localization.GetLoc.texts = new List<LoadLocText>();
        SceneManager.LoadSceneAsync(2);
    }

    public void AudioMode()
    {
        AudioListener.volume = audio.isOn ? 1 : 0;
        Globals.global.Audio = audio.isOn;
        PlayerPrefs.SetInt("audioStg", audio.isOn ? 1 : 0);
    }

    public void WindowMode()
    {
        Screen.fullScreen = window.isOn ? true : false;
        Globals.global.Window = window;
        PlayerPrefs.SetInt("windowStg", window.isOn ? 1 : 0);
    }

    public void Language()
    {
        if ((int)Localization.GetLoc.CurrentLang < Enum.GetNames(typeof(Lang)).Length - 1)
            Localization.GetLoc.CurrentLang++;
        else
            Localization.GetLoc.CurrentLang = 0;

        PlayerPrefs.SetInt("lang", (int)Localization.GetLoc.CurrentLang);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
