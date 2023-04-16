using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class button1 : MonoBehaviour
{
    
    
    [SerializeField] private TextMeshProUGUI answersFile;
    [SerializeField] private TextMeshProUGUI answer1;
    
    public void WriteButton1()
    {
        if (GameManager._instance.gameRun)
        {
            answersFile.text += "--" + answer1.text;
            GameManager._instance.choseAnswer = answer1.text;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
       
    }
}
