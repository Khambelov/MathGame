using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour {

    public Text variant;

    public void CheckAnswer()
    {
        if (variant.text == Generator.gameGenerator.result.ToString())
        {
            AnswersManager.game.RightAnswer();
        }
        else
        {
            AnswersManager.game.WrongAnswer();
        }

    }
}
