using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    List<char> operations = new List<char>
    {
        '+','-','*','/'
    };

    public static Generator gameGenerator;

    public Image problemBorder;
    public Text problemText;
    public List<Text> variants;

    public int result;

    int coef;

    private void Awake()
    {
        gameGenerator = this;
    }

    // Use this for initialization
    void Start()
    {
        coef = 0;
        CheckDifficultyLevel();
    }

    void CheckDifficultyLevel()
    {
        switch (Globals.global.Mode)
        {
            case "easy": { coef = 10; EasyGen(); break; }
            case "medium": { coef = 5; MediumGen(); break; }
            case "hard": { coef = 3; HardGen(); break; }
        }
    }

    void EasyGen()
    {
        int x = Random.Range(0, 101);
        int y = Random.Range(0, 101);
        char operation = operations[Random.Range(0, 2)];
        result = GetResult(x, y, operation);

        AnswersManager.game.problemsCount++;

        problemBorder.color = new Color(255, 255, 255, 1);
        problemText.text = string.Concat(x, " ", operation, " ", y);

        variants[Random.Range(0, variants.Count)].text = result.ToString();

        foreach (Text t in variants)
        {
            t.gameObject.transform.parent.GetComponent<Button>().interactable = true;
            if (int.Parse(t.text) != result)
            {
                t.text = "0";
                do
                {
                    t.text = Random.Range(result - coef, result + coef).ToString();
                }
                while (t.text == result.ToString() || variants.FindAll(text => text.text == t.text && text != t).Count != 0);
            }
        }

        ChangeCoef();
    }

    void MediumGen()
    {
        int x = Random.Range(0, 501);
        int y = Random.Range(0, 501);
        char operation = operations[Random.Range(0, 4)];

        if (operation == '/')
            if (GetResult(x, y, operation) == -1)
            {
                MediumGen();
                return;
            }

        result = GetResult(x, y, operation);

        AnswersManager.game.problemsCount++;

        problemBorder.color = new Color(255, 255, 255, 1);
        problemText.text = string.Concat(x, " ", operation, " ", y);

        variants[Random.Range(0, variants.Count)].text = result.ToString();
        foreach (Text t in variants)
        {
            t.gameObject.transform.parent.GetComponent<Button>().interactable = true;
            if (int.Parse(t.text) != result)
            {
                t.text = "0";
                do
                {
                    t.text = Random.Range(result - coef, result + coef).ToString();
                }
                while (t.text == result.ToString() || variants.FindAll(text => text.text == t.text && text != t).Count != 0);
            }
        }

        ChangeCoef();
    }

    void HardGen()
    {
        int x = Random.Range(0, 501);
        int y = Random.Range(0, 501);
        int a = Random.Range(0, 501);
        int b = Random.Range(0, 501);
        char operation1 = operations[Random.Range(0, 4)];
        char operation2 = operations[Random.Range(0, 4)];
        char operation3 = operations[Random.Range(0, 4)];

        if (operation1 == '/')
            if (GetResult(x, y, operation1) == -1)
            {
                HardGen();
                return;
            }

        if (operation2 == '/')
            if (GetResult(a, b, operation2) == -1)
            {
                HardGen();
                return;
            }

        if (operation3 == '/')
            if (GetResult(GetResult(x, y, operation1), GetResult(a, b, operation2), operation3) == -1)
            {
                HardGen();
                return;
            }

        result = GetResult(GetResult(x, y, operation1), GetResult(a, b, operation2), operation3);

        AnswersManager.game.problemsCount++;

        problemBorder.color = new Color(255, 255, 255, 1);
        problemText.text = string.Concat("(", x, " ", operation1, " ", y, ")", " ", operation3, " ", "(", a, " ", operation2, " ", b, ")");

        variants[Random.Range(0, variants.Count)].text = result.ToString();
        foreach (Text t in variants)
        {
            t.gameObject.transform.parent.GetComponent<Button>().interactable = true;
            if (int.Parse(t.text) != result)
            {
                t.text = "0";
                do
                {
                    t.text = Random.Range(result - coef, result + coef).ToString();
                }
                while (t.text == result.ToString() || variants.FindAll(text => text.text == t.text && text != t).Count != 0);
            }
        }

        ChangeCoef();
    }

    void ChangeCoef()
    {
        switch (Globals.global.Mode)
        {
            case "easy": coef = Random.Range(coef / 3, coef * 3 + 1); break;
            case "medium": coef = Random.Range(coef / 2, coef * 2 + 1); break;
            case "hard": coef = Random.Range(coef - 26, coef + 26); break;
        }

        if (coef <= 0)
            switch (Globals.global.Mode)
            {
                case "easy": coef = 10; break;
                case "medium": coef = 5; break;
                case "hard": coef = 3; break;
            }
    }

    int GetResult(int x, int y, char operation)
    {
        switch (operation)
        {
            case '+': return x + y;
            case '-': return x - y;
            case '*': return x * y;
            case '/':
                {
                    if (y != 0)
                    {
                        if ((float)((float)x / (float)y) % 1 == 0)
                            return x / y;
                        else
                            return -1;
                    }
                    else return 0;
                }
            default: return 0;
        }
    }

    public void RegenerateProblem()
    {
        CheckDifficultyLevel();
    }
}
