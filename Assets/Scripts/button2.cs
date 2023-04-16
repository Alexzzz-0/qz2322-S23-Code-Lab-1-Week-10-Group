using System.Collections;
using System.Collections.Generic;using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class button2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI answersFile;
    [SerializeField] private TextMeshProUGUI answer2;

    public void WroteButton2()
    {
        if (GameManager._instance.gameRun)
        {
            answersFile.text += "--" + answer2.text;
            GameManager._instance.choseAnswer = answer2.text;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
    }
}
