using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{

    public static Globals global;

    public bool activate = false;

    public Record bestPlayers;

    private string nickName;
    public string Name { get { return nickName; } set { nickName = value; } }

    private string mode = null;
    public string Mode { get { return mode; } set { mode = value; } }


    private bool audio = true;
    public bool Audio { get { return audio; } set { audio = value; } }

    private bool window = true;
    public bool Window { get { return window; } set { window = value; } }

    private void Start()
    {
        if (!activate)
        {
            if (global == null)
            {
                global = this;
                DontDestroyOnLoad(gameObject);
                activate = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (Preload.preload)
        {
            Preload.preload.globalIsReady = true;
        }
    }
}
