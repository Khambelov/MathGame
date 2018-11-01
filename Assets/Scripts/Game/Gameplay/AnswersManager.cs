using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnswersManager : MonoBehaviour
{

    public static AnswersManager game;

    public Image border;
    public Text problemText;
    public Text rightAnswersText;
    public List<Button> variants;

    public int problemsCount;
    public int rightAnswers;
    public int wrongAnswers;

    int rightCombo;
    int wrongCombo;

    private void Awake()
    {
        game = this;
    }

    // Use this for initialization
    private void Start()
    {
        problemsCount = 0;
        rightAnswers = 0;
        wrongAnswers = 0;
        rightCombo = 0;
        wrongCombo = 0;
    }

    public void RightAnswer()
    {
        foreach (Button b in variants)
            b.interactable = false;

        rightAnswers++;
        rightAnswersText.text = rightAnswers.ToString();
        rightCombo++;
        wrongCombo = 0;

        border.color = new Color(0, 255, 0, 1);
        problemText.text =  Localization.GetLoc.GetLocById(12);

        if (rightCombo == 5)
        {
            rightCombo = 0;
            TimeManager.timer.gameTime += 5;
            TimeManager.timer.ChangeNewTime();
        }

        StartCoroutine(WaitBeforeRenegate());
    }

    public void WrongAnswer()
    {
        foreach (Button b in variants)
            b.interactable = false;

        wrongAnswers++;
        wrongCombo++;
        rightCombo = 0;

        border.color = new Color(255, 0, 0, 1);
        problemText.text = Localization.GetLoc.GetLocById(13);

        if (wrongCombo == 3)
        {
            wrongCombo = 0;
            TimeManager.timer.gameTime -= 5;
            TimeManager.timer.ChangeNewTime();
        }

        StartCoroutine(WaitBeforeRenegate());
    }

    IEnumerator WaitBeforeRenegate()
    {
        yield return new WaitForSeconds(0.5f);

        Generator.gameGenerator.RegenerateProblem();

        yield return null;
    }

    public void EndGame()
    {
        FinishGame.end.Show(problemsCount, rightAnswers);
    }
}
