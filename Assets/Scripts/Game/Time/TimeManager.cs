using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager timer;

    public GameObject prepareWindow;
    public Text prepareText;
    public Text gameTimeText;

    float prepareTime = 4f;
    int time = 60;
    public int gameTime { get { return time; } set { time = value; } }
    string prepare = "Get Ready!";

    private void Awake()
    {
        timer = this;
    }

    // Use this for initialization
    void Start()
    {
        gameTimeText.text = "01 : 00";
        StartCoroutine(StartTimer());
    }

    public void Restart()
    {
        prepareText.text = prepare;
        prepareTime = 4f;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        prepareWindow.SetActive(true);
        while (prepareTime > 3f)
        {
            prepareTime--;
            yield return new WaitForSeconds(1f);
        }

        while (prepareTime > 0f)
        {
            prepareText.text = prepareTime.ToString();
            prepareTime--;
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(Timer());

        yield return null;
    }

    IEnumerator Timer()
    {
        prepareWindow.SetActive(false);

        int minutes = 0;
        int seconds = 0;

        while (time > -1)
        {
            minutes = time / 60;
            seconds = time - (minutes != 0 ? (60 * minutes) : 0);

            if (time > 0)
                gameTimeText.text = string.Concat(minutes < 10 ? "0" : "", minutes, " : ", seconds < 10 ? "0" : "", seconds);
            else
                gameTimeText.text = "00 : 00";
            time--;

            yield return new WaitForSeconds(1f);
        }

        gameTimeText.text = "00 : 00";

        AnswersManager.game.EndGame();

        yield return null;
    }

    public void ChangeNewTime()
    {
        int minutes = 0;
        int seconds = 0;

        minutes = time / 60;
        seconds = time - (minutes != 0 ? (60 * minutes) : 0);

        if (time > 0f)
            gameTimeText.text = string.Concat(minutes < 10 ? "0" : "", minutes, " : ", seconds < 10 ? "0" : "", seconds);
        else
            gameTimeText.text = "00 : 00";
    }
}
