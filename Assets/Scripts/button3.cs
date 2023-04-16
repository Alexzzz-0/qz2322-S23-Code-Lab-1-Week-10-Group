using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class button3 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI answersFile;
    [SerializeField] private TextMeshProUGUI answer3;

    public void WroteButton3()
    {
        if (GameManager._instance.gameRun)
        {
            answersFile.text += "--" + answer3.text;
            GameManager._instance.choseAnswer = answer3.text;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
