using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{

    public static GamePause pause;

    public GameObject darker;
    public AudioSource sound;

    private void Awake()
    {
        pause = this;
    }

    public void StartPause()
    {
        darker.SetActive(true);
        TimeManager.timer.StopAllCoroutines();
        sound.Pause();
    }

    public void ExitGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
