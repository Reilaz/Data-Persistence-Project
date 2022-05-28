using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    //public static Menu Instance;
    public TMP_InputField getName;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        LoadScore();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InputName()
    {
        string name = getName.text;
        Debug.Log(name);
    }
    public void StartGame()
    {
        Data.Instance.name = getName.text;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        //MainManager.Instance.SaveColor();
        Data.Instance.SaveMaxScore(); 
        if (true)
        {
            EditorApplication.ExitPlaymode();    
        }
        else
        {
            Application.Quit();
        }    
    }
    public void LoadScore()
    {
        Data.Instance.LoadMaxScore();
        scoreText.text = "Best Score: " + Data.Instance.namePlayer + " : " + Data.Instance.maxScore;
    }

}
