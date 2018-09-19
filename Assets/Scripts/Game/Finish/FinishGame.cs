using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishGame : MonoBehaviour
{

    public static FinishGame end;

    public GameObject darker;
    public Animator animator;
    public Text stats;
    public Text status;

    private void Awake()
    {
        end = this;
    }

    public void Show(int count, int rights)
    {
        darker.SetActive(true);
        animator.SetBool("end", true);
        stats.text = string.Concat(rights, " / ", count, "; Winrate: ", Mathf.RoundToInt(((float)rights / (float)count) * 100f), "%");
        status.text = GetStatus(((float)rights / (float)count) * 100f);
    }

    string GetStatus(float winrate)
    {
        if (winrate == 100f)
        {
            status.color = new Color(0, 0, 0, 1);
            return "You are a mathematical genius !!!";
        }
        else if (winrate >= 0f && winrate <= 50f)
        {
            status.color = new Color(255, 0, 0, 1);
            return "You obviously need to tighten up your skills!";
        }
        else if (winrate > 50f && winrate <= 75f)
        {
            status.color = new Color(255, 255, 0, 1);
            return "Not bad, but it is possible much better.";
        }
        else if (winrate > 75f && winrate < 100f)
        {
            status.color = new Color(0, 255, 0, 1);
            return "Your skills are good, but there is something to strive for.";
        }

        status.color = new Color(255, 255, 255, 1);
        return "The developer is so stupid that he could not properly implement the status output.";
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Exit()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
