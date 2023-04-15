using System.Collections;
using System.Collections.Generic;using TMPro;
using UnityEngine;



public class button2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI answersFile;
    [SerializeField] private TextMeshProUGUI answer2;

    public void WroteButton2()
    {
        if (!GameManager._instance.gameRun)
        {
            return;
        }
        answersFile.text += "--" + answer2.text;
        GameManager._instance.choseAnswer = answer2.text;
    }
}
