using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public InputField name;

    public void Correct()
    {
        if (name.text.Length > 0 && !char.IsLetter(name.text[name.text.Length - 1]))
        {
            name.text = name.text.TrimEnd(name.text[name.text.Length - 1]);
        }
    }
}
