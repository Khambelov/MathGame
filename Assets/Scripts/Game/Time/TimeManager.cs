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

    public int sessionTime;
    public float speed;

    float prepareTime = 4f;
    int time = 60;
    public int gameTime { get { return time; } set { time = value; } }
    string prepare = Localization.GetLoc.GetLocById(10);

    private void Awake()
    {
        timer = this;
    }

    // Use this for initialization
    void Start()
    {
        gameTimeText.text = "01 : 00";
        sessionTime = 0;
        prepareText.text = prepare;
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

        while (time > 0)
        {
            minutes = time / 60;
            seconds = time - (minutes != 0 ? (60 * minutes) : 0);

            if (time > 0)
                gameTimeText.text = string.Concat(minutes < 10 ? "0" : "", minutes, " : ", seconds < 10 ? "0" : "", seconds);
            else
                gameTimeText.text = "00 : 00";

            time--;
            sessionTime++;

            if (sessionTime == 59)
            {
                speed = ((float)AnswersManager.game.rightAnswers / (float)sessionTime);
                speed = ((int)(speed * 100)) / 100f;
            }

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
