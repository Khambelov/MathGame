using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Preload : MonoBehaviour
{

    public static Preload preload;

    public Text loadBar;
    public GameObject window;
    public InputField nickname;
    public Button confirm;
    public Text text;

    public bool globalIsReady;
    public bool locaizationIsReady;

    Dictionary<int, string> loadLang = new Dictionary<int, string>
    {
        { 0,"Загрузка"},
        { 1, "Loading"},
        { 2, "Төяү"}
    };

    private void Awake()
    {
        preload = this;
        globalIsReady = false;
        locaizationIsReady = false;
    }

    private void Start()
    {
        StartCoroutine(Loading());
        StartCoroutine(WaitingLoad());

    }

    public void Confirm()
    {
        if (nickname.text.Length > 0)
        {
            Globals.global.Name = nickname.text;
            Localization.GetLoc.texts = new List<LoadLocText>();
            SceneManager.LoadScene(1);
        }
        else
        {
            StartCoroutine(Invalid());
        }
    }

    void OpenWIndow()
    {
        window.SetActive(true);
    }

    IEnumerator WaitingLoad()
    {
        while (!globalIsReady && !locaizationIsReady)
        {
            yield return null;
        }

        loadBar.gameObject.SetActive(false);
        OpenWIndow();

        yield return null;
    }

    IEnumerator Loading()
    {
        if (PlayerPrefs.HasKey("lang"))
            loadBar.text = loadLang[PlayerPrefs.GetInt("lang")];
        else
            loadBar.text = loadLang[1];

        yield return new WaitForSeconds(0.5f);

        loadBar.text += '.';

        yield return new WaitForSeconds(0.5f);

        loadBar.text += '.';

        yield return new WaitForSeconds(0.5f);

        loadBar.text += '.';

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Loading());

        yield return null;
    }

    IEnumerator Invalid()
    {
        text.color = Color.red;
        confirm.interactable = false;
        text.text = Localization.GetLoc.GetLocById(34);

        yield return new WaitForSeconds(1f);

        text.color = Color.white;
        confirm.interactable = true;
        text.text = Localization.GetLoc.GetLocById(33);

        yield return null;
    }
}

