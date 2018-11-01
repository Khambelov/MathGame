using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishGame : MonoBehaviour
{

    public static FinishGame end;

    public GameObject darker;
    public ScrollRect winList;
    public Button prev;
    public Button next;
    public Text correctAnswers;
    public Text totalAnswers;
    public Text gameTime;
    public Text winrate;
    public Text result;
    public List<BestPlayerField> bestPlayers;
    public List<Image> stars;

    bool loadStars;
    int wr;


    private void Awake()
    {
        end = this;
        loadStars = false;
        wr = 0;
    }

    public void Show(int count, int rights)
    {
        float timeCoef = ((float)count / (float)TimeManager.timer.sessionTime);
        timeCoef = timeCoef > 1f ? 1 : timeCoef;

        wr = Mathf.RoundToInt((((float)rights / (float)count) * timeCoef) * 100f);
        wr = wr > 100 ? 100 : wr;

        darker.SetActive(true);
        darker.GetComponent<Animator>().SetTrigger("end");

        if (TimeManager.timer.speed > 0)
            Globals.global.bestPlayers.AddBestPlayer(Globals.global.Name, wr, rights, TimeManager.timer.speed);

        #region Fill Data
        correctAnswers.text = rights.ToString();
        totalAnswers.text = count.ToString();
        gameTime.text = GetGameTime(TimeManager.timer.sessionTime);
        winrate.text = string.Concat(Localization.GetLoc.GetLocById(17), ' ', wr, "%");
        result.text = GetStatus(wr);
        GetBestPlayerList();
        #endregion
    }

    public void Restart()
    {
        Localization.GetLoc.texts = new List<LoadLocText>();
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        Localization.GetLoc.texts = new List<LoadLocText>();
        SceneManager.LoadScene(1);
    }

    public void Next()
    {
        StartCoroutine(NextWin());
    }

    IEnumerator NextWin()
    {
        float toScroll = 0f;

        next.interactable = false;
        prev.interactable = false;

        prev.gameObject.SetActive(true);

        if (winList.horizontalScrollbar.value >= 0.5f)
        {
            next.gameObject.SetActive(false);
        }

        while (toScroll < 0.5f)
        {
            Debug.Log("next");
            winList.horizontalScrollbar.value += 0.05f;

            toScroll += 0.05f;

            yield return new WaitForSeconds(0.01f);
        }

        if (!loadStars)
        {
            yield return StartCoroutine(LoadStars());
        }

        next.interactable = true;
        prev.interactable = true;

        yield return null;
    }

    public void Prev()
    {
        StartCoroutine(PrevWin());
    }

    IEnumerator PrevWin()
    {
        float toScroll = 0f;

        prev.interactable = false;
        next.interactable = false;
        next.gameObject.SetActive(true);

        if (winList.horizontalScrollbar.value <= 0.5f)
        {
            prev.gameObject.SetActive(false);
        }

        while (toScroll < 0.5f)
        {
            winList.horizontalScrollbar.value -= 0.05f;

            toScroll += 0.05f;

            yield return new WaitForSeconds(0.01f);
        }

        prev.interactable = true;
        next.interactable = true;
        yield return null;
    }

    IEnumerator LoadStars()
    {
        int temp = 20;

        for (int i = 0; i < stars.Count; i++)
        {
            if (temp > wr)
            {
                break;
            }

            while (stars[i].fillAmount < 1f)
            {
                stars[i].fillAmount += 0.01f;

                yield return new WaitForSeconds(0.01f);
            }

            temp += 20;
        }

        loadStars = true;
        yield return null;
    }

    string GetStatus(int winrate)
    {
        if (winrate == 100f)
        {
            result.color = new Color(0, 0, 0, 1);
            return Localization.GetLoc.GetLocById(19);
        }
        else if (winrate >= 0f && winrate <= 50f)
        {
            result.color = new Color(255, 0, 0, 1);
            return Localization.GetLoc.GetLocById(20);
        }
        else if (winrate > 50f && winrate <= 75f)
        {
            result.color = new Color(255, 255, 0, 1);
            return Localization.GetLoc.GetLocById(21);
        }
        else if (winrate > 75f && winrate < 100f)
        {
            result.color = new Color(0, 255, 0, 1);
            return Localization.GetLoc.GetLocById(22);
        }

        result.color = new Color(255, 255, 255, 1);
        return "The developer is so stupid that he could not properly implement the status output.";
    }

    string GetGameTime(int time)
    {
        TimeSpan ts = TimeSpan.FromSeconds(time);

        return string.Format("{0:D2}:{1:D2}:{2:D2}",
            ts.Hours,
            ts.Minutes,
            ts.Seconds);
    }

    void GetBestPlayerList()
    {
        BestPlayer[] players = Globals.global.bestPlayers.GetPlayerList();

        for (int i = 0; i < players.Length; i++)
        {
            if (!String.IsNullOrEmpty(players[i].pName))
            {
                bestPlayers[i].nickname.text = players[i].pName;
                bestPlayers[i].winrate.text = players[i].winrate.ToString() + "%";
                bestPlayers[i].answers.text = players[i].correctCount.ToString();
                bestPlayers[i].speed.text = players[i].speed.ToString() + Localization.GetLoc.GetLocById(37);
            }
            else
            {
                bestPlayers[i].nickname.text = "";
                bestPlayers[i].winrate.text = "";
                bestPlayers[i].answers.text = "";
                bestPlayers[i].speed.text = "";
            }
        }
    }
}

[System.Serializable]
public struct BestPlayerField
{
    public GameObject field;
    public Text nickname;
    public Text winrate;
    public Text answers;
    public Text speed;
}