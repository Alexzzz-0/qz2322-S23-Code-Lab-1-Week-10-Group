using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class button3 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI answersFile;
    [SerializeField] private TextMeshProUGUI answer3;

    public void WroteButton3()
    {
        if (!GameManager._instance.gameRun)
        {
            return;
        }
        answersFile.text += "--" + answer3.text;
        GameManager._instance.choseAnswer = answer3.text;
    }
}
