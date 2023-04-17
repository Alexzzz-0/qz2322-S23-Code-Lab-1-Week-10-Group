using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI question;
    public TextMeshProUGUI button1;
    public TextMeshProUGUI button2;
    public TextMeshProUGUI button3;
    public TextMeshProUGUI answers;
    [SerializeField] private TextMeshProUGUI beautiful;

    [SerializeField] private GameObject face;

    public ScriptableObjects object1;
    public ScriptableObjects object2;
    public ScriptableObjects object3;
    public ScriptableObjects object4;
    public ScriptableObjects object5;
    public ScriptableObjects object6;
    public ScriptableObjects object7;
    public ScriptableObjects object8;
    
    private Dictionary<string, ScriptableObjects> ScriDic = new Dictionary<string, ScriptableObjects>();
    

    private int qArandom = 0;

    public string choseAnswer;

    public static GameManager _instance;

    public bool gameRun = true;

    public float mulAmt;

    
    //an array to hold all the question B blanks (location possibility)
    private string[] questionB = new string[] { "above", "under", "left to", "right to" };
    //a list to hold all the organs
    private List<string> organ = new List<string>() { "left eye", "left eye bow", "right eye", "right eye bow", "nose", "mouth","left ear","right ear"};
    
    //a 2D list to hold all the info for organ map
    private List<List<string>> OrganMap = new List<List<string>>();
    
    
    private void Start()
    {
        _instance = this;
        
        //initialize the array for sciptable objects
        ScriDic.Add("left eye bow",object1);
        ScriDic.Add("right eye bow",object2);
        ScriDic.Add("left eye",object3);
        ScriDic.Add("right eye",object4);
        ScriDic.Add("left ear",object5);
        ScriDic.Add("right ear",object6);
        ScriDic.Add("nose",object7);
        ScriDic.Add("mouth",object8);
        
        //_object = object1;
        // _sprite = leftEye;
        choseAnswer = null;
        
        InitAllScriObject();
        
    }
    

    string qAString;
    private string qBString;
    private int questionIndex = 0;

    private float qAX;
    private float qAy;

    private int qax;
    private int qay;
    
    //triggered by button
    public void choose()
    {
        if (gameRun)
        {
            //write the organ in question & organ in answer's position into files
            WriteIntoFile();
        
            //check the end state of the game
            //generate the organs in the face now
            questionIndex++;
        
            if (questionIndex >= 6)
            {
                //give an random position to the organs that have never been chosen
                CheckNullFile();

                question.text = null;
                beautiful.text = "You are so beautiful~";
                button1.text = "Restart";
                button2.text = "Restart";
                button3.text = "Restart";
            
                //generate organs
                GenerateOrganInFace();
            
                //turn gameRun to false
                //so the scripts in the button cannot write more button text into answer column
                gameRun = false;
                return;
            }

            DisplayNew();
        }
    }

    
    
        

    private int _listNum = 0;
    
    void InitAllScriObject()
    {
        //initialize all the object's position info
        //make them all (0,0) when game first starts
        foreach (var ScriObj in ScriDic)
        {
            ScriObj.Value.x = 0;
            ScriObj.Value.y = 0;
        }
    }

    void WriteIntoFile()
    {
        //the first time player choose a button to start
        //no need to write into file
        if (questionIndex == 0)
        {
            return;
        }
        
        
        if (questionIndex == 1)
        {
            //this is for scriptable objects:
            //set the position of the organ in the first question as (10,10) the initial point
            //the followings will be built around that
            ScriDic[qAString].x = 10f;
            ScriDic[qAString].y = 10f;
            
            //this is for position map 2D list:
            //put the organ in the first question into the 2D list as initial point
            //the followings will be built around that
            List<string> list0 = new List<string>();
            OrganMap.Add(list0);
            list0.Add(qAString);
            qax = 0;
            qay = 0;
        }
        
        qAX = ScriDic[qAString].x;
        qAy = ScriDic[qAString].y;
        
        //if it is left to
        if (qBString.Equals("left to"))
        {
            LeftTo(choseAnswer);
        }
        
        //what is right to
        if (qBString.Equals("right to"))
        {
           RightTo(choseAnswer);
        }
        
        //what is above
        if (qBString.Equals("above"))
        {
            Above(choseAnswer);
        }
        
        //what is under
        if (qBString.Equals("under"))
        {
            Under(choseAnswer);
        }
        
    }

    void CheckNullFile()
    {
        foreach (var file in ScriDic)
        {
            
            if (file.Value.x == 0)
            {
                //Debug.Log(file.Key);
                int randomDir = Random.Range(0, 4);
                switch (randomDir)
                {
                    case 0:
                        LeftTo(file.Key);
                        break;
                    case 1:
                        RightTo(file.Key);
                        break;
                    case 2:
                        Above(file.Key);
                        break;
                    case 3:
                        Under(file.Key);
                        break;
                }
            }
        }

        organ.Remove(choseAnswer);
        OrganMap.Insert(0,new List<string>());
        OrganMap[0].Add(organ[0]);
        int cap = OrganMap.Count-1;
        OrganMap[cap].Add(organ[1]);

        // foreach (var organ in organ)
        // {
        //     Debug.Log(organ);
        // }
        
        // Debug.Log(organ[0]);
        // Debug.Log(organ[1]);

        for (int i = 0; i < OrganMap.Count; i++)
        {
            Debug.Log(i);
            for (int j = 0; j < OrganMap[i].Count; j++)
            {
                Debug.Log(OrganMap[i][j]);
            }
        }

    }

    void LeftTo(string answerOrgan)
    {
        //write in scriptable objects
        foreach (var dirOrgan in ScriDic)
        {
            if (dirOrgan.Value.x < qAX && dirOrgan.Value.y == qAy)
            {
                qAX = dirOrgan.Value.x;
            }

            ScriDic[answerOrgan].x = qAX - 1f;
            ScriDic[answerOrgan].y = qAy;
        }

        //write in 2D list
        int temp = qax - 1;
        if (temp<0)
        {
            OrganMap.Insert(qax,new List<string>());
            OrganMap[qax].Add(answerOrgan);
            qay = OrganMap[qax].Count - 1;
        }
        else
        {
            OrganMap[temp].Add(answerOrgan);
            qax = temp;
            qay = OrganMap[temp].Count - 1;
        }
            
    }

    void RightTo(string answerOrgan)
    {
        //write into scriptable objects
        foreach (var dirOrgan in ScriDic)
        {
            if (dirOrgan.Value.x > qAX && dirOrgan.Value.y == qAy )
            {
                qAX = dirOrgan.Value.x;
            }
            ScriDic[answerOrgan].x = qAX + 1f;
            ScriDic[answerOrgan].y = qAy;
        }
        
        //write into 2D list
        int temp = qax + 1;
        if (temp>OrganMap.Count-1)
        {
            OrganMap.Add(new List<string>());
            OrganMap[temp].Add(answerOrgan);
            qax = temp;
            qay = OrganMap[temp].Count - 1;
        }
        else
        {
            OrganMap[temp].Add(answerOrgan);
            qax = temp;
            qay = OrganMap[temp].Count - 1;
        }
    }

    void Above(string answerOrgan)
    {
        //write into scriptable objects
        foreach (var dirOrgan in ScriDic)
        {
            if (dirOrgan.Value.y > qAy && dirOrgan.Value.x == qAX )
            {
                qAy = dirOrgan.Value.y;
            }
            ScriDic[answerOrgan].x = qAX;
            ScriDic[answerOrgan].y = qAy + 1f;
        }

        //write into 2d list
        OrganMap[qax].Insert(qay,choseAnswer);
        
        
    }

    void Under(string answerOrgan)
    {
        //write into scriptable objects
        foreach (var dirOrgan in ScriDic)
        {
            if (dirOrgan.Value.y < qAy && dirOrgan.Value.x == qAX )
            {
                qAy = dirOrgan.Value.y;
            }
            ScriDic[answerOrgan].x = qAX;
            ScriDic[answerOrgan].y = qAy - 1f;
        }
        
        //write into 2d list
        OrganMap[qax].Insert(qay+1,choseAnswer);
    }
    
    void DisplayNew()
    {
        //make the chosen answer the next question
        //left eye, right eye...
        qAString = choseAnswer;

        //randomly choose one direction (up/left/down/right)
        //every time we press the button
        int qBRandom;
        qBRandom = Random.Range(0, questionB.Length - 1);
        qBString = questionB[qBRandom];

        //display the question with A and B blank
        question.text = "What is " + qBString + " the " + qAString +"?";

        //wrote the question into display
        answers.text += "\n" + questionIndex + question.text;
        
        //randomly choose an organ as the answer
        //delete the element of list for now, so three answers won't be the same
        //add back at the end
        int answerRandomIndex;

        organ.Remove(qAString);
        answerRandomIndex = Random.Range(0, organ.Count - 1);
        button1.text = organ[answerRandomIndex];
        organ.RemoveAt(answerRandomIndex);
        answerRandomIndex = Random.Range(0, organ.Count - 1);
        button2.text = organ[answerRandomIndex];
        organ.RemoveAt(answerRandomIndex);
        answerRandomIndex = Random.Range(0, organ.Count - 1);
        button3.text = organ[answerRandomIndex];
        organ.Add(button1.text);
        organ.Add(button2.text);
        
    }

    public float startX;
    public float startY;
    public float gapX;
    public float gapY;
    private float alignX;

    void GenerateOrganInFace()
    {
        //according to the position information in organ map
        //generate the organs in the face here
        
        //according to the width of all the organs
        //adjust the starting position X
        alignX = (OrganMap.Count - 3) * gapX/2;

        for (int i = 0; i < OrganMap.Count; i++)
        {
            for (int q = 0; q < OrganMap[i].Count; q++)
            {
                string _name = OrganMap[i][q];
                GameObject newObj = Instantiate(ScriDic[_name].organs);
                //make the first and last line a slot lower
                if (i == 0 || i == OrganMap.Count - 1)
                {
                    newObj.transform.position = new Vector3(startX - alignX + i * gapX, startY - gapY - gapY * q);
                }
                else
                {
                    newObj.transform.position = new Vector3(startX - alignX + gapX * i, startY - gapY * q);
                }
            }
        }
        
        //adjust face's size
        face.transform.localScale += new Vector3((OrganMap.Count - 2) * mulAmt, 0, 0);
    }
    
}
