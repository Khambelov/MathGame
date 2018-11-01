using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFIxer : MonoBehaviour
{
    public Text btn_end_retry;
    public Text btn_end_exit;

    // Update is called once per frame
    void Update()
    {
        btn_end_exit.fontSize = btn_end_retry.cachedTextGenerator.fontSizeUsedForBestFit;
    }
}
