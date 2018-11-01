using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLocText : MonoBehaviour
{
    [SerializeField]
    private int id;

    private void Start()
    {
        LoadLoc();

        if (!Localization.GetLoc.texts.Contains(this))
            Localization.GetLoc.texts.Add(this);
    }

    public void LoadLoc()
    {
        GetComponent<Text>().text = Localization.GetLoc.GetLocById(id);
    }
}
